import validate from 'validate.js'
import validation from './ValidationEntities';

export function validateField(fieldName, value) {

    let formValues = {};
    let formFields = {};

    formValues[fieldName] = value;

    formFields[fieldName] = validation[fieldName];

    const result = validate(formValues, formFields);

    if (result) {
        return result[fieldName][0];
    }

    return null;
}

export function validateForm(form, errors) {
    let result = {
        isValid: true,
        errors: errors
    };
    
    for (const property in form) {
    
        if (Object.prototype.hasOwnProperty.call(form, property)) {
            result.errors[property] = null;

            let error = validateField(property, form[property]);

            if (error) {
                result.errors[property] = error;

                if (result.isValid) {
                    result.isValid = false;
                }
            }
        }
    }
    
    return result;
}