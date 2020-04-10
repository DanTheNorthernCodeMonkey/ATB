import React from 'react';
import './App.css';
import { BeanAdvertForm } from "./BeanAdvertForm";
import 'bootstrap/dist/css/bootstrap.min.css';
import Container from "react-bootstrap/Container";
import LoadingOverlay from 'react-loading-overlay';

export class App extends React.Component {

  constructor(props) {
    super(props)

    this.state = {
      isActive: false
    }

    this.loading = this.loading.bind(this);
  }

  loading(isActive) {
    this.setState({ isActive: isActive });
  }

  render() {
    return (
      <div className="App">
        <LoadingOverlay
          active={this.state.isActive}
          spinner
          text=''
        >
          <Container>
            <h1>Bean Advert Upload</h1>
            <BeanAdvertForm loading={this.loading} />
          </Container>
        </LoadingOverlay>
      </div>
    );
  }
}