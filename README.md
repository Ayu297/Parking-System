Console Application for Parking System Management Using .NET

A console application in C# and .NET 6.0 for managing a virtual parking system. Allows users to allocate parking slots, park vehicles based on registration number and color, and generate various parking reports.
Requirements

    .NET 6.0 SDK
    JetBrains Rider 2024.1.3

Installation
Clone this repository

git clone https://github.com/ayu297/Parking-System.git

Navigate to the project directory

cd Parking-System

Usage
Build the project

dotnet build

Run the application

dotnet run

    Follow the on-screen instruction to use different commands like park, leave, status, etc.

Menu 

Welcome to Parking Menu
Choose an option:
1) Create Parking Lot
2) Park Vehicle
3) Leave Parking Lot
4) Get Parking Status
5) Count Vehicles By Type
6) Get Vehicles By Odd Plate
7) Get Vehicles By Even Plate
8) Get Vehicles By Color
9) Get Slot By Color
10) Get Report
0) Exit

Command Input Format
Create a parking lot

1 <totalLot>

Park a Vehicle

2 <resgistrationNo> <color> <vehicle>

Leave Parking Lot

3 <slotNumber>

Show Parking Lot Status

4

Show Total Vehicle By Type

5 <vehicle>

Show Registration Number By Odd Plate

6

Show Resgistration Number By Even Plate

7

Show Resgistration Number By Color

8

Show Slot Number By Color

9

Report

10

Exit Command

0
