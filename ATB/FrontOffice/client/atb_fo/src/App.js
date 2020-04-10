import React from 'react';
import logo from './logo.svg';
import './App.css';
import { BeanAdvert } from "./BeanAdvert";
import LoadingOverlay from 'react-loading-overlay';


export class App extends React.Component {

  constructor(props) {
    super(props)

    this.state = {
      isActive: true
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
          text='Loading...'
        >
          <header className="App-header">
            <BeanAdvert loading={this.loading}/>
          </header>
        </LoadingOverlay>
      </div>
    );
  }
}

export default App;
