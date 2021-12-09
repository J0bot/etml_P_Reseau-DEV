CREATE DATABASE IF NOT EXISTS db_employees;

USE db_employees;

CREATE TABLE IF NOT EXISTS regions
(
region_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
region_name VARCHAR(25),
PRIMARY KEY(region_id)
);

CREATE TABLE IF NOT EXISTS jobs
(
job_id VARCHAR(10) NOT NULL,
job_title VARCHAR(35),
min_salary DECIMAL(8,0),
max_salary DECIMAL(8,0),
PRIMARY KEY(job_id)
);

CREATE TABLE IF NOT EXISTS countries
(
country_id CHAR(2) NOT NULL,
country_name VARCHAR(40),
region_id INT,
PRIMARY KEY(country_id)
);

CREATE TABLE IF NOT EXISTS locations
location_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
street_address VARCHAR(40),
postal_code VARCHAR(12),
city VARCHAR(30),
state_province VARCHAR(25),
country_id CHAR(2),
PRIMARY KEY(location_id)
);

CREATE TABLE IF NOT EXISTS departments
(
department_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
department_name VARCHAR(30),
manager_id INT,
location_id INT,
PRIMARY KEY(department_id)
);

CREATE TABLE IF NOT EXISTS employees
(
employee_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
first_name VARCHAR(20),
last_name VARCHAR(25),
email VARCHAR(25),
phone_number VARCHAR(20),
hire_date DATE,
job_id VARCHAR(10),
salary DECIMAL(8,2),
commission_pct DECIMAL(2,2),
manager_id INT,
department_id INT,
PRIMARY KEY(employee_id)
);

CREATE TABLE IF NOT EXISTS job_history
(
employee_id INT,
start_date DATE,
end_date DATE,
job_id VARCHAR(10),
department_id INT,
PRIMARY KEY(employee_id, job_id)
);

ALTER TABLE `countries` ADD CONSTRAINT `region_fk` FOREIGN KEY (`region_id`) REFERENCES `regions`(`region_id`) ON DELETE CASCADE ON UPDATE CASCADE;
 
ALTER TABLE `locations` ADD CONSTRAINT `country_fk` FOREIGN KEY (`country_id`) REFERENCES `country`(`country_id`) ON DELETE CASCADE ON UPDATE CASCADE;
