CREATE TABLE Beans (
    id UUID PRIMARY KEY,
    image_id UUID NOT NULL,
    cost NUMERIC (5,2) NOT NULL,
    bean_name TEXT NOT NULL,
    aroma TEXT NOT NULL,
    colour TEXT NOT NULL,
    date TIMESTAMP NOT NULL
);

ALTER USER postgres WITH PASSWORD 'postgres';