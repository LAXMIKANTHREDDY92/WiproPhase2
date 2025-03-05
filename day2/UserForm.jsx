import React, { Component } from 'react'

export default class UserForm extends Component {
    
    constructor(props){
        super(props);
        this.num1=React.createRef();
        this.num2=React.createRef();
        
    }
    Submit=(event)=>{
        event.preventDefault();
        console.log(this.num1.current.value)
        console.log(this.num2.current.value)
        console.log(parseInt(this.num1.current.value)+parseInt(this.num2.current.value))

        

    }

  render() {
    return (
      <div>
        <form onSubmit={this.Submit}>
        <label >Num1</label>
        <input type="text" ref={this.num1}  />
        <label >Num2</label>
        <input type="text" ref={this.num2}  />
        <button type="submit">Submit</button>
        </form>

        
      </div>
    )
  }
}
