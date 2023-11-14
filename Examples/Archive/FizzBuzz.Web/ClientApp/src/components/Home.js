import React, { Component } from 'react';
import { Link } from "react-router-dom";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>FizzBuzz Numbers Demo!</h1>
        <p>How would you like to play today?</p>
        <p><Link to="predefined-rules">With pre-defined rules.</Link></p>
        <p><Link to="custom-rules">With custom rules.</Link></p>
      </div>
    );
  }
}
