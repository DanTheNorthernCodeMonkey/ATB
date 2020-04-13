import React from "react";
import axios from "axios";
import Row from "react-bootstrap/Row";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import './BeanAdvert.css';
import {DatePickerCalendar} from 'react-nice-dates'
import {enGB, da} from 'date-fns/locale'
import isPast from 'date-fns/isPast'
import isToday from 'date-fns/isToday'
import parse from 'date-fns/parse'
import isEqual from 'date-fns/isEqual'
import 'react-nice-dates/build/style.css'
import {validateForm} from "./ValidationWrapper";

export class BeanAdvertForm extends React.Component {

    constructor(props) {

        super(props);

        this.state = {
            form: {
                cost: 12.00,
                name: 'Best Bean',
                aroma: 'Sweet',
                colour: 'Black',
                image: null,
                date: enGB.date,
            },
            uploadSuccess: undefined,
            errors: {
                cost: null,
                name: null,
                aroma: null,
                colour: null,
                image: null,
                date: null,
            },
            isActive: false,
            takenDates: []
        };

        this.submit = this.submit.bind(this);
        this.handleCalendar = this.handleCalendar.bind(this);
        this.disableDates = this.disableDates.bind(this);
        this.dateToJsonDate = this.dateToJsonDate.bind(this);
    }

    componentDidMount() {

        this.props.loading(true);
        
        axios
            .get('http://localhost:5000/BeanAdvertAdmin', {})
            .then(response => {
                console.log(response);
                if (response.status === 200) {
                    this.setState({takenDates: response.data});
                }
            })
            .catch(error => {
                console.log(error);
            })
            .finally(() => {
                this.props.loading(false);
            });
    }

    submit() {
        this.props.loading(true);

        const validationResult = validateForm(this.state.form, this.state.errors);
        this.setState({errors: validationResult.errors});

        if (!validationResult.isValid) {
            this.props.loading(false);
            return;
        }

        let date = this.dateToJsonDate(this.state.form.date);

        axios
            .post('http://localhost:5000/BeanAdvertAdmin', {
                Cost: this.state.form.cost,
                Name: this.state.form.name,
                Aroma: this.state.form.aroma,
                Colour: this.state.form.colour,
                Date: date,
                Image64: this.state.form.image
            })
            .then(response => {
                this.submissionCallback(response);
            })
            .catch(error => {
                console.log(error);

                let dateStatus = "Error";

                if (error.response && error.response.data) {
                    dateStatus = error.response.data.dateStatus;
                }

                this.setState({uploadSuccess: false, dateStatus: dateStatus})
            })
            .finally(() => {
                this.props.loading(false);
            });
    }

    submissionCallback(response) {
        let takenDates;

        if (response.data.takenDate) {
            takenDates = this.state.takenDates;
            takenDates.push(response.data.takenDate);
        }

        if (response.status === 200) {
            this.setState({
                cost: 0.00,
                name: '',
                aroma: '',
                colour: '',
                image: undefined,
                uploadSuccess: true,
                dateStatus: response.dateStatus,
                takenDates: takenDates,
                date: enGB.date
            });
        }

        this.forceUpdate();
    }

    handleChange = ({target}) => {
        this.setFormState([target.name], target.value);
    };

    handleCost = (costFormValue) => {
        let cost = parseFloat(costFormValue);
        this.setFormState('cost', cost.value);
    };

    handleFile = event => {

        const image = event.target.files[0];
        this.setState({image: image});

        const self = this;

        const reader = new FileReader();
        reader.readAsDataURL(image);
        reader.onload = function () {
            const b64 = reader.result.replace(/^data:.+;base64,/, '');
            self.setFormState('image', b64);
        };
    };

    handleCalendar = date => {
        console.log(date);
        this.setFormState('date', date);
    };

    setFormState(target, value) {
        let form = this.state.form;

        form[target] = value;

        this.setState({form: form});
    }

    dateToJsonDate(date) {

        let month = date.getMonth();
        month = month + 1;
        if (month < 10) {
            month = '0' + month;
        }

        let day = date.getDate();
        if (day < 10) {
            day = '0' + day;
        }

        return date.getFullYear() + '-' + month + '-' + day + 'T00:00:00';
    }


    disableDates(date) {

        if (isPast(date)) {
            return !isToday(date);
        }

        let takenDates = this.state.takenDates;

        if (takenDates.length === 0) {
            return false;
        }

        for (let i = 0; i < takenDates.length; i++) {

            const iso8601Date = takenDates[i].substring(0, 10);
            const formattedDate = parse(iso8601Date, 'yyyy-MM-dd', new Date());
            if (isEqual(date, formattedDate)) {
                return true;
            }
        }
        return false;
    }

    render() {

        const modifiers = {
            disabled: date => this.disableDates(date),
            highlight: date => this.disableDates(date),
        };

        const modifiersClassNames = {
            highlight: '-highlight'
        };

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
                <Form useNativeValidation>
                    <Form.Group>
                        <Row>
                            <Col>
                                <Form.Label>
                                    Cost £
                                </Form.Label>
                                <Form.Control
                                    name="cost"
                                    type="number"
                                    value={this.state.form.cost}
                                    onChange={this.handleCost}
                                    isInvalid={this.state.errors.cost}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {this.state.errors.cost}
                                </Form.Control.Feedback>
                            </Col>
                            <Col>
                                <Form.Label>
                                    Name
                                </Form.Label>
                                <Form.Control
                                    name="name"
                                    type="text"
                                    value={this.state.form.name}
                                    onChange={this.handleChange}
                                    isInvalid={this.state.errors.name}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {this.state.errors.name}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Form.Label>
                                    Aroma
                                </Form.Label>
                                <Form.Control
                                    name="aroma"
                                    type="text"
                                    value={this.state.form.aroma}
                                    onChange={this.handleChange}
                                    isInvalid={this.state.errors.aroma}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {this.state.errors.aroma}
                                </Form.Control.Feedback>
                            </Col>
                            <Col>
                                <Form.Label>
                                    Colour
                                </Form.Label>
                                <Form.Control
                                    name="colour"
                                    type="text"
                                    value={this.state.form.colour}
                                    onChange={this.handleChange}
                                    isInvalid={this.state.errors.colour}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {this.state.errors.colour}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>

                        <Row>
                            <Col>
                                <Form.Label className="mt-3">
                                    Choose a date!
                                </Form.Label>
                                <div className="calendar">
                                    <DatePickerCalendar
                                        locale={enGB}
                                        onDateChange={this.handleCalendar}
                                        date={this.state.form.date}
                                        modifiers={modifiers}
                                        modifiersClassNames={modifiersClassNames}
                                    />
                                </div>
                                <div className="invalid-custom">
                                    {this.state.errors.date}
                                </div>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Form.Label className="mt-3">
                                    Image
                                </Form.Label>
                                <div className="file-upload">
                                    <input name="image" type="file" onChange={this.handleFile} required/>
                                </div>
                                <div className="invalid-custom mb-4 ">
                                    {this.state.errors.image}
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
        )
    }
}