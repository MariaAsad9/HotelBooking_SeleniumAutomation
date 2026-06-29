# End-to-End Automation Testing – Adactin Hotel Application

## Project Overview

This project automates the core functionalities of the Adactin Hotel Application using Selenium WebDriver with C#. The automation covers the complete hotel booking workflow, including login, hotel search, booking, confirmation, and logout.

**Application Under Test:**

https://adactinhotelapp.com/

---

## Technologies Used

- C#
- Selenium WebDriver
- NUnit
- ChromeDriver
- Visual Studio
- .NET Framework
- Git & GitHub

---

## Project Structure

```text
HotelBooking_SeleniumAutomation/
│
├── SeleniumAutomation.sln
│
├── SeleniumAutomation/
│   ├── Properties/
│   ├── app.config
│   ├── packages.config
│   ├── CorePage.cs
│   ├── LoginPage.cs
│   ├── TestExecution.cs
│   └── SeleniumAutomation.csproj
│
└── TestResults/
```

---

## Objective

The objective of this project is to automate the end-to-end hotel booking process and validate both positive and negative scenarios using assertions.

The following modules are covered:

- User Login
- Hotel Search
- Hotel Selection
- Hotel Booking
- Booking Confirmation
- User Logout

---

## Features Tested

### Login Module

- Valid login
- Invalid login
- Error message verification

### Search Hotel Module

- Hotel search with valid data
- Mandatory field validation
- Navigation checks

### Booking Module

- Hotel selection
- Personal information validation
- Payment validation
- Booking confirmation

### Logout Module

- Successful logout
- Session validation

---

## How to Run the Project

### Clone the Repository

```bash
git clone https://github.com/MariaAsad9/HotelBooking_SeleniumAutomation.git
```

### Open Solution

Open:

```text
SeleniumAutomation.sln
```

using Visual Studio.

### Restore Packages

Restore all NuGet packages from:

```text
packages.config
```

### Run Tests

Execute the tests using:

- Visual Studio Test Explorer
- NUnit Test Runner

---

## Expected Results

- Positive test cases should execute successfully.
- Negative scenarios should display appropriate validation messages.
- Booking IDs should be generated only for successful bookings.
- Users should be redirected properly after logout.

---

## Author

Maria Asad
