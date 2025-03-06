import React from "react";
import First from "./Components/First";
import Second from "./Components/Second";
import Third from "./Components/Third";
import Fourth from "./Components/Fourth";
import First1 from "./Components/First1";
import Second1 from "./Components/Second1";
import Third1 from "./Components/Third1";
import Fourth1 from "./Components/Fourth1";
import Student from "./Components/Student";
import Student1 from "./Components/Student1";
import Display from "./Components/Display";


const App = () => {
  const handleHello = () => alert("Hello");
  const handleBye = () => alert("Bye");

  return (
    <div>
      <h1>Hello World</h1>

      {/* Function Components */}
      <First />
      <Second />
      <Third />
      <Fourth />

      {/* Class Components */}
      <First1 />
      <Second1 />
      <Third1 />
      <Fourth1 />

      {/* Passing Props to Student Components */}
      <Student name="laxmi" address="123 city" scores={[96, 65, 88]} />
      <Student1 name="kanth" address="232 Avenue" scores={[50, 75, 82]} />

      {/* Display Component with Editable Inputs */}
      <Display name="reddy" address="789 kjvjn" />

      {/* { Buttons with onClick Events */} 
      <button onClick={handleHello}>Say Hello</button>
      <button onClick={handleBye}>Say Bye</button>
    </div>
  );
};

export default App;
