# Calculator

A Shunting Yard algorithm console-based calculator written in **C# (.NET 8)**.  
Supports basic arithmetic operations such as addition, subtraction, multiplication, and division.

---

## 🚀 Features
- ➕ Addition  
- ➖ Subtraction  
- ✖️ Multiplication  
- ➗ Division (with division by zero handling)
- () Parentheses
- -/+ Unary signs
- 📜 Clean and readable C# code  

---

## 📂 Project Structure
Calculator/
│── Program.cs # Entry point
│── Calculator.cs # Core calculator logic
│── ExpressionParser.cs # Parser for string input
|── Exceptions/*.cs # Errors catching
|── HistoryManager.cs # History of actions
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
- AST algorithm (Abstract Syntax Tree) instead of Shunting Yard
- Add advanced math functions (power, square root, etc.)
- GUI version with Blazor or WinForms