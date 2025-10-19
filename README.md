Claim management System Prototype
Overview
This repository contains the source code for a non-functional GUI prototype of a Contract Monthly Claim System (CMCS). This project was developed as Part 1 of the Portfolio of Evidence (PoE) for PROG6212. The prototype is designed to provide a visual representation of the proposed system, focusing on user-friendly and intuitive design principles without implementing the backend functionality.

Features
The GUI prototype is designed to visually demonstrate the following key functionalities for different user roles:

Claim Submission: A form for lecturers (independent contractors) to easily submit their monthly claims with the option to attach supporting documents.

Claim Tracking: A view for lecturers to transparently track the status of their submitted claims.

Claim Approval: An interface for Programme Coordinators and Academic Managers to verify and approve submitted claims. The design includes a data grid to list pending claims and a separate window for viewing full claim details and managing the approval process.

Technology Stack
The prototype is built using the following technologies:

Language: C#

Framework: .NET Core

GUI: Windows Presentation Foundation (WPF)

Project Structure
The project is organized into logical components to separate the GUI from the data models.

MainWindow.xaml and MainWindow.xaml.cs: The main application window containing the tabbed interface for all user tasks.

ClaimDetailsWindow.xaml and ClaimDetailsWindow.xaml.cs: A separate window to display the detailed information of a selected claim for approval.

Claim.cs: A C# class representing the data model for a claim.

User.cs: A C# class representing the data model for a user (contractor, lecturer, etc.).

How to Run
Since this is a non-functional prototype, you can run it directly from Visual Studio.

Open the solution file (.sln) in Visual Studio.

Set MainWindow.xaml as the startup window.

Click Run or press F5.