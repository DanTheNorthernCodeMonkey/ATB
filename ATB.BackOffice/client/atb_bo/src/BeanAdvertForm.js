import React from "react";
import axios from "axios";
import {Calendar} from "react-calendar";
import 'react-calendar/dist/Calendar.css'
import Row from "react-bootstrap/Row";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import './BeanAdvert.css';


export class BeanAdvertForm extends React.Component {

    constructor(props) {

        super(props);

        this.state = {
            cost: 12.00,
            name: 'Best Bean',
            aroma: 'Sweet',
            colour: 'Black',
            image: null,
            date: null,
            uploadSuccess: undefined
        };

        this.submit = this.submit.bind(this);
    }

    submit() {
        axios
            .post('http://localhost:5000/BeanAdvertAdmin', {
                Cost: this.state.cost,
                Name: this.state.name,
                Aroma: this.state.aroma,
                Colour: this.state.colour,
                Date: this.state.date,
                Image64: this.state.base64Image
            })
            .then(response => {
                this.submissionCallback(response);
            })
            .catch(error => {
                console.log(error);
                
                if (error.response.data){
                    const dateStatus = error.response.data.dateStatus;
                    this.setState({uploadSuccess: false, dateStatus: dateStatus})
                }
            });
    }

    submissionCallback(response) {
        if (response.status === 200) {
            this.setState({
                    cost: 0.00,
                    name: '',
                    aroma: '',
                    colour: '',
                    image: undefined,
                    uploadSuccess: true,
                    dateStatus: response.dateStatus
                }
            );
        }
    }

    handleChange = ({target}) => this.setState({[target.name]: target.value});

    handleFile = event => {

        const image = event.target.files[0];
        this.setState({image: image});
        
        const self = this;

        const reader = new FileReader();
        reader.readAsDataURL(image);
        reader.onload = function(){
            const b64 = reader.result.replace(/^data:.+;base64,/, '');
            self.setState({base64Image: b64})
        };
    };

    handleCalendar = date => this.setState({date: date});

    render() {
        return (
            <div>
                {
                    this.state.uploadSuccess !== undefined && this.state.uploadSuccess === false &&
                    <div className="banner error">
                        <h2>Upload failed.</h2>
                        {
                            this.state.dateStatus === "Taken" &&
                            <p>That date had been taken. Please try another date</p>
                        }
                        {
                            this.state.dateStatus === "Past" &&
                            <p>That date is in the past. Please select today or a date in the future.</p>
                        }
                        {
                            this.state.dateStatus === "Error" &&
                            <p>Please try again. If the problem persists please don't hesitate to contact IT support</p>
                        }
                    </div>   
                }
                {
                    this.state.uploadSuccess === true &&
                    <div className="banner success">
                        <h2>Upload succeeded!</h2>
                    </div>
                }
                <Form>
                    <Form.Group>
                        <Row>
                            <Col>
                                <Form.Label>
                                    Cost £
                                </Form.Label>
                                <Form.Control name="cost" type="text" value={this.state.cost}
                                              onChange={this.handleChange}/>
                            </Col>
                            <Col>
                                <Form.Label>
                                    Name
                                </Form.Label>
                                <Form.Control name="name" type="text" value={this.state.name}
                                              onChange={this.handleChange}/>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Form.Label>
                                    Aroma
                                </Form.Label>
                                <Form.Control name="aroma" type="text" value={this.state.aroma}
                                              onChange={this.handleChange}/>
                            </Col>
                            <Col>
                                <Form.Label>
                                    Colour
                                </Form.Label>
                                <Form.Control name="colour" type="text" value={this.state.colour}
                                              onChange={this.handleChange}/>
                            </Col>
                        </Row>
                        
                        <Row>
                            <Col>

                                <Form.Label className="mt-3">
                                    Choose a date!
                                </Form.Label>
                                <div className="mb-5margin-auto">
                                    <Calendar onChange={this.handleCalendar} />
                                </div>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Form.Label className="mt-3">
                                Image
                                </Form.Label>
                                <div className="file-upload mb-4 ">
                                    <input name="image" type="file" onChange={this.handleFile}/>
                                </div>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button variant="primary" size="lg" block onClick={this.submit}>
                                    Submit
                                </Button>
                            </Col>
                        </Row>
                    </Form.Group>
                </Form>
            </div>
        );
    }
}