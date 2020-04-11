const validation = {
    cost: {
        presence: {
            allowEmpty: false,
        },
        numericality: {
            message: '^Please enter a valid cost'
        }
    },

    name: {
        presence: {
            allowEmpty: false,
            message: '^Please enter a name'
        },
        length: {
            maximum: 50,
            message: '^Bean name cannot be more than 50 characters'
        }
    },

    aroma: {
        presence: {
            allowEmpty: false,
            message: '^Please enter an aroma'
        },
        length: {
            maximum: 50,
            message: '^Aroma cannot be more than 50 characters'
        }
    },

    image: {
        presence: {
            allowEmpty: false,
            message: '^Please enter an image'
        }
    },

    date: {
        presence: {
            allowEmpty: false,
            message: '^Please select a date'
        }
    },

    colour: {
        presence: {
            allowEmpty: false,
            message: '^Please select a colour'
        }
    },
};

export default validation;