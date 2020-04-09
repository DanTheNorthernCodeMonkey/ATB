import React from 'react';
import logo from './logo.svg';
import './App.css';
import {BeanAdvertForm} from "./BeanAdvertForm";
import 'bootstrap/dist/css/bootstrap.min.css';
import Container from "react-bootstrap/Container";

function App() {
  return (
    <div className="App">
      <header className="App-header">
          <Container>
              <h1>Bean Advert Upload</h1>
              <BeanAdvertForm />
          </Container>
      </header>
    </div>
  );
}

export default App;
