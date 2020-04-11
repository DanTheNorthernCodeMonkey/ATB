import validate from 'validate.js'
import validation from './ValidationEntities';

export default function validateField(fieldName, value) {

    let formValues = {};
    let formFields = {};

    formValues[fieldName] = value;

    formFields[fieldName] = validation[fieldName];

    const result = validate(formValues, formFields);

    if (result) {
        return result[fieldName][0];
    }

    return null;
};