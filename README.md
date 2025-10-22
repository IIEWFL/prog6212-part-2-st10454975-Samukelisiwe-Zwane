Claim Management System
Overview

The Claim Management System is a C# WPF application developed as part of the PROG6212 Portfolio of Evidence (PoE). The project was initially created as a non-functional graphical user interface (GUI) prototype in Part 1 and later extended in Part 2 to include functional features and testing.

The system enables lecturers (independent contractors) to submit and track monthly claims for their work. Programme Coordinators and Academic Managers can review, verify, and approve or reject these claims. The application focuses on user-friendly design, system reliability, and clear separation between data models and presentation logic.

Features
Lecturer Features

Submit Claims: Lecturers can submit monthly claims by entering details such as claim month, hours worked, and supporting documentation.

Track Submitted Claims: Submitted claims are displayed in an organized table format, showing claim month, hours worked, submission date, and current status.

Coordinator and Manager Features

View Pending Claims: Coordinators and managers have a dedicated section to review pending claims.

Approve or Reject Claims: Each claim includes options to approve or reject. The process updates the claim status in real time.

Claim Details View: A separate details window provides additional information for each claim and allows managers to confirm approval or rejection.

System Features

Data Validation and Error Handling: The application validates inputs to prevent invalid or incomplete submissions. Meaningful error messages are displayed when necessary.

Unit Testing: Core system functions are tested using the xUnit framework to ensure reliability and consistent performance.

Technology Stack

The Claim Management System is developed using the following technologies:

Language: C#

Framework: .NET Core

User Interface: Windows Presentation Foundation (WPF)

Testing Framework: xUnit

Integrated Development Environment: Visual Studio 2022

Project Structure

The project is structured into logical components to maintain clarity and modularity:

App.xaml: Defines global application resources and the startup window.

MainWindow.xaml and MainWindow.xaml.cs: The main interface that provides access to all system functionalities, including claim submission, viewing, and approval.

ClaimDetailsWindow.xaml and ClaimDetailsWindow.xaml.cs: A secondary window for displaying detailed claim information and managing approval actions.

Claim.cs: Represents the data model for claims, including lecturer name, claim month, hours worked, and status.

ClaimTest.cs: Contains xUnit test cases to verify the functionality of claim submission and approval processes.

User.cs: Contains the user data structure from the initial prototype (optional for later implementation).

Running the Application

Open the solution in Visual Studio.

Set MainWindow.xaml as the startup window.

Build and run the project by pressing F5 or selecting Start.

The application opens with three primary tabs:

Submit Claim

View Claims

Approve Claims

Each tab provides a specific user workflow within the system.

Running Unit Tests

Open the Test Explorer in Visual Studio.

Locate and select ClaimTest.cs under the test project.

Right-click and select Run Tests.

Verify that all tests execute successfully and pass without errors.