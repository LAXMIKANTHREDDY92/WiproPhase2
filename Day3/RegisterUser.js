import React from 'react'
import { Formik, Form, Field, ErrorMessage} from 'formik'
import * as Yup from "yup";

// to do validation, we create Schema for the object
 
const RegisterUser = () => {
  const UserSchema =
  Yup.object().shape(
    {
      name:Yup.string().required('Name is must').min(10,"Min 10 charatcters needed").max(20, "Max 20 characters are allowed"),
      
      password:Yup.string().required('password is must').min(10,"Min 10 charatcters needed"),
      gender:Yup.string().required('gender is must')


    }
  )
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
        
     } 
    validationSchema={UserSchema} 
    component={RegisterForm}
    onSubmit={(values)=>
         console.log(values)
    }>
    </Formik>
    </div>)}


    
export function RegisterForm()
{
  return(

     <Form>
        <div>
    <label> Name </label>
    <Field name="name" type="text"/> 
    <ErrorMessage name='name'/>
    </div>

    <div>
    <label> Address </label>
    <Field name="address" type="text"/> 
    </div>

    <div>
    <label> Password </label>
    <Field name="password" type="text"/> 
    <ErrorMessage name="password"/>
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
    <ErrorMessage name="gender"/>
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
     
  )
}

export default RegisterUser;
