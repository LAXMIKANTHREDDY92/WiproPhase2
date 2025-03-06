import React, { Component, createRef } from "react";

class AddTwoNumbers extends Component {
  constructor(props) {
    super(props);
    this.num1Ref = createRef();
    this.num2Ref = createRef();
    this.state = { sum: 0 };
  }

  handleAddition = (event) => {
    event.preventDefault();
    const num1 = parseFloat(this.num1Ref.current.value) || 0;
    const num2 = parseFloat(this.num2Ref.current.value) || 0;
    
    this.setState({ sum: num1 + num2 });
  };

  render() {
    return (
      <div className="container">
        <h2>Add Two Numbers using UnContrlledComponent:</h2>
        <form onSubmit={this.handleAddition}>
          <input type="number" placeholder="Enter first number" ref={this.num1Ref} />
          <input type="number" placeholder="Enter second number" ref={this.num2Ref} />
          <button type="submit">Add</button>
        </form>
        <h3>Result: {this.state.sum}</h3>
      </div>
    );
  }
}

export default AddTwoNumbers;
