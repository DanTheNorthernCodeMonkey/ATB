import React from "react";

export class BeanInfo extends React.Component {

    render() {
        return (
            <div>
                <p>Cost: £{this.props.cost}</p>
                <p>Aroma: {this.props.aroma}</p>
                <p>Colour: {this.props.colour}</p>
            </div>
        );
    }
}