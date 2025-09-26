# Calculator

A simple console-based calculator written in **C# (.NET 8)**.  
Supports basic arithmetic operations such as addition, subtraction, multiplication, and division.

---

## 🚀 Features
- ➕ Addition  
- ➖ Subtraction  
- ✖️ Multiplication  
- ➗ Division (with division by zero handling)  
- 📜 Clean and readable C# code  

---

## 📂 Project Structure
Calculator/
│── Program.cs # Entry point
│── Calculator.cs # Core calculator logic
│── ExpressionParser.cs # Parser for string input
│── README.md

## ⚙️ Installation & Run
### 1. Clone the repository:
```bash
git clone https://github.com/m01ves2/ConsoleCalculator.git
cd Calculator

## 📂 Build and run
dotnet run --project Calculator

## 📌 Usage Example
The program asks the user for input and performs the operation:
3 + 4 / 2 - 7
Result: -2

## 🗺️ Roadmap
- Support for negative numbers
- Support for parentheses ( )
- Implement Dijkstra's Shunting-yard algorithm for expression parsing
- Add advanced math functions (power, square root, etc.)
- GUI version with Blazor or WinForms