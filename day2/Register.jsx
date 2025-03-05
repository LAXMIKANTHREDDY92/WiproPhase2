import React from 'react'
import { Formik, Form, Field} from 'formik'

const Register = () => {

  return (
    <div> <h1> Register Form </h1>
    <Formik 
    initialValues=
    {
        {name :"",
         address :"",
         gender :"",
         email : "",
         password:"",
         slot :"",
         dob :"",
         about:""
        }
        
    } onSubmit={(values)=>
         console.log(values)
    }>
    <Form>
        <div>
    <label> Name </label>
    <Field name="name" type="text"/> 
    </div>

    <div>
    <label> Address </label>
    <Field name="address" type="text"/> 
    </div>

    <div>
    <label> Password </label>
    <Field name="password" type="text"/> 
    </div>

    <div>
    <label> DOb </label>
    <Field name="dob" type="date"/> 
    </div>

    <div>
    <label> About yourself </label>
    <Field name="about" type="textarea"/> 
    </div>

    <div>
    <label> Gender </label>
    Male 
    <Field name="gender" type="radio" value="male"/> 
    Female 
    <Field name="gender" type="radio" value="female"/> 
    
    </div>
    <div>
    <label> Select Slot </label>
      
    <Field name="slot" as="select"> 
    <option> 11-1 </option>
    <option> 1-4</option>
    </Field> 
    </div>

    <button type='submit'> Register User </button>
    </Form>    
    </Formik>
    </div>
  )
}

export default Register;
