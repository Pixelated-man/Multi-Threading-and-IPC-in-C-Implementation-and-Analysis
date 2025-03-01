# Multi-Threading and IPC in C#: Implementation and Analysis

## Overview
This project demonstrates the implementation of a multi-threaded banking application and an inter-process communication (IPC) solution using named pipes in C#. The multi-threaded application simulates concurrent customer transactions while ensuring thread safety, synchronization, and deadlock prevention. The IPC solution implements a producer-consumer model using named pipes to facilitate communication between two processes.

### Key Features
1. **Multi-Threaded Banking Application**:
   - Simulates concurrent customer transactions (withdrawals, deposits, and transfers).
   - Ensures thread safety using `lock` keyword for synchronization.
   - Implements deadlock prevention using resource ordering.
   - Demonstrates concurrency, synchronization, and stress testing.

2. **IPC Solution**:
   - Uses named pipes for communication between producer and consumer processes.
   - Implements a producer-consumer model with structured message passing.
   - Includes error handling and performance benchmarking.

---

## Dependencies
To build and run this project, you will need the following dependencies:

### 1. **.NET SDK**
   - The project requires the .NET SDK to compile and run. It has been tested with **.NET 6** and **.NET 8**.
   - Download and install the appropriate version for your operating system from the [official .NET website](https://dotnet.microsoft.com/download).

### 2. **Visual Studio Code (Optional)**
   - Recommended for development and debugging.
   - Download and install from the [official VS Code website](https://code.visualstudio.com/).

### 3. **C# Extension for VS Code (Optional)**
   - Required for debugging in Visual Studio Code.
   - Install the C# extension from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).

---

## Installation Instructions

### Step 1: Install .NET SDK
#### On Ubuntu
1. Add Microsoft's package repository:
   ```bash
   wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   rm packages-microsoft-prod.deb
