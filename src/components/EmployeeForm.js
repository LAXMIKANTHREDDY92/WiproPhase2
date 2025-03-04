import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";

const EmployeeForm = ({ initialValues, onSubmit }) => {
  const validationSchema = Yup.object({
    name: Yup.string().required("Required"),
    address: Yup.string().required("Required"),
    dept: Yup.string().required("Required"),
    manager: Yup.string().required("Required"),
  });

  return (
    <div>
      <h2>Add/Edit Employee</h2>
      <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
        {() => (
          <Form>
            <div className="mb-2">
              <Field name="name" placeholder="Name" className="form-control" />
              <ErrorMessage name="name" component="div" className="text-danger" />
            </div>
            <div className="mb-2">
              <Field name="address" placeholder="Address" className="form-control" />
              <ErrorMessage name="address" component="div" className="text-danger" />
            </div>
            <div className="mb-2">
              <Field name="dept" placeholder="Department" className="form-control" />
              <ErrorMessage name="dept" component="div" className="text-danger" />
            </div>
            <div className="mb-2">
              <Field name="manager" placeholder="Manager" className="form-control" />
              <ErrorMessage name="manager" component="div" className="text-danger" />
            </div>
            <button type="submit" className="btn btn-primary">Save</button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default EmployeeForm;
