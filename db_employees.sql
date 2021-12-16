DROP DATABASE IF EXISTS db_employees;

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
job_title VARCHAR(35) NOT NULL,
min_salary DECIMAL(8,0),
max_salary DECIMAL(8,0),
PRIMARY KEY(job_id)
);

CREATE TABLE IF NOT EXISTS countries
(
country_id CHAR(2) NOT NULL,
country_name VARCHAR(40),
region_id INT UNSIGNED NOT NULL,
PRIMARY KEY(country_id)
);

CREATE TABLE IF NOT EXISTS locations
(
location_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
street_address VARCHAR(40),
postal_code VARCHAR(12),
city VARCHAR(30) NOT NULL,
state_province VARCHAR(25),
country_id CHAR(2) NOT NULL,
PRIMARY KEY(location_id)
);

CREATE TABLE IF NOT EXISTS departments
(
department_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
department_name VARCHAR(30) NOT NULL,
location_id INT UNSIGNED,
PRIMARY KEY(department_id)
);

CREATE TABLE IF NOT EXISTS employees
(
employee_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
first_name VARCHAR(20),
last_name VARCHAR(25) NOT NULL,
email VARCHAR(25) NOT NULL,
phone_number VARCHAR(20),
hire_date DATE NOT NULL,
job_id VARCHAR(10) NOT NULL,
salary DECIMAL(8,2) NOT NULL,
commission_pct DECIMAL(2,2),
department_id INT UNSIGNED,
PRIMARY KEY(employee_id)
);

CREATE TABLE IF NOT EXISTS job_history
(
employee_id INT UNSIGNED NOT NULL,
start_date DATE NOT NULL,
end_date DATE NOT NULL,
job_id VARCHAR(10) NOT NULL,
department_id INT UNSIGNED NOT NULL,
PRIMARY KEY(employee_id, job_id, department_id)
);

ALTER TABLE `countries` ADD CONSTRAINT `cou_region_fk` FOREIGN KEY (`region_id`) REFERENCES `regions`(`region_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `locations` ADD CONSTRAINT `loc_country_fk` FOREIGN KEY (`country_id`) REFERENCES `countries`(`country_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `departments` ADD CONSTRAINT `dep_location_fk` FOREIGN KEY (`location_id`) REFERENCES `locations`(`location_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `employees` ADD CONSTRAINT `emp_job_fk` FOREIGN KEY (`job_id`) REFERENCES `jobs`(`job_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `employees` ADD CONSTRAINT `emp_department_fk` FOREIGN KEY (`department_id`) REFERENCES `departments`(`department_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `job_history` ADD CONSTRAINT `jhist_employee_fk` FOREIGN KEY (`employee_id`) REFERENCES `employees`(`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `job_history` ADD CONSTRAINT `jhist_job_fk` FOREIGN KEY (`job_id`) REFERENCES `jobs`(`job_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `job_history` ADD CONSTRAINT `jhist_department_fk` FOREIGN KEY (`department_id`) REFERENCES `departments`(`department_id`) ON DELETE CASCADE ON UPDATE CASCADE;