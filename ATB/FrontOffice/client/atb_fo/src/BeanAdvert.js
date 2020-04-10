import React from "react";
import axios from "axios";
import { BeanInfo } from "./BeanInfo";
import Spinner from "react-bootstrap/Spinner";
import './bean.css';

export class BeanAdvert extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            loading: true,
            showBeanInfo: false,
            beanAdvert: {
                beanName: '',
                cost: 0.00,
                aroma: '',
                colour: '',
                imageUrl: '',
                success: false
            }
        };

        this.setBeanInfoShow = this.setBeanInfoShow.bind(this);
    }

    componentDidMount() {

        this.props.loading(true);

        axios
            .get('http://localhost:5000/BeanAdvert', {})
            .then(response => {
                console.log(response);
                if (response.status === 200) {
                    this.setState({ beanAdvert: response.data, success: true, loading: false });
                }
            })
            .catch(error => {
                console.log(error);
                this.setState({ success: false, loading: false })
            })
            .finally(() => {
                this.props.loading(false);
            });
    }

    setBeanInfoShow() {
        this.setState({ showBeanInfo: !this.state.showBeanInfo });
    }

    render() {
        return (
            <div>
                {
                    !this.state.loading && !this.state.success &&
                    <div>
                        <h1>Oh no!</h1>
                        <p>There's been an issue. Please try back later.</p>
                    </div>
                }
                {
                    !this.state.loading && this.state.success && this.state.beanAdvert.beanName == null &&
                    <div>
                        <h1>Oh no!</h1>
                        <p>It seems we've sold out of beans. Check back tomorrow.</p>
                    </div>
                }
                {
                    !this.state.loading && this.state.success && this.state.beanAdvert.beanName != null &&
                    <div className="bean-wrapper" onClick={this.setBeanInfoShow}>
                        <h1>Bean of the day</h1>
                        <h3>{this.state.beanAdvert.beanName}</h3>
                        <img src={process.env.PUBLIC_URL + this.state.beanAdvert.imageUri} alt="bean" className="bean-img" />
                        {
                            this.state.showBeanInfo &&
                            <BeanInfo
                                cost={this.state.beanAdvert.cost}
                                aroma={this.state.beanAdvert.aroma}
                                colour={this.state.beanAdvert.colour}
                            />
                        }
                    </div>
                }
            </div>
        );
    }
}
