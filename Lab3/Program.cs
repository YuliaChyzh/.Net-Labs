/* Labwork 3
 * Variat 17
 * Autor Yulia Chyzh
 * 
 * Паттерн "Команда" (Command) позволяет инкапсулировать запрос на выполнение определенного действия в виде отдельного объекта.
 * Этот объект запроса на действие и называется командой. При этом объекты, инициирующие запросы на выполнение действия, отделяются от объектов,
 * которые выполняют это действие.
 * Когда использовать команды?
 * Когда надо передавать в качестве параметров определенные действия, вызываемые в ответ на другие действия.
 * То есть когда необходимы функции обратного действия в ответ на определенные действия.
 * Когда необходимо обеспечить выполнение очереди запросов, а также их возможную отмену.
 * Когда надо поддерживать логгирование изменений в результате запросов. Использование логов может помочь восстановить состояние системы -
 * для этого необходимо будет использовать последовательность запротоколированных команд.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create user
            User user = new User();

            // Let him do anything.
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            // Сancel 4 commands
            user.Undo(4);

            // Return 3 commands which was cancel
            user.Redo(3);

            // Waiting for the user's enter and finish.
            Console.Read();
        }
    }

    // "Command": abstract command
    abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    // "ConcreteCommand" : concrete command

    class CalculatorCommand : Command
    {
        char @operator;
        int operand;
        Calculator calculator;

        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        }

        public char Operator
        {
            set { @operator = value; }
        }

        public int Operand
        {
            set { operand = value; }
        }

        public override void Execute()
        {
            calculator.Operation(@operator, operand);
        }

        public override void UnExecute()
        {
            calculator.Operation(Undo(@operator), operand);
        }

        // Private helper function: 
        private char Undo(char @operator)
        {
            char undo;
            switch (@operator)
            {
                case '+': undo = '-'; break;
                case '-': undo = '+'; break;
                case '*': undo = '/'; break;
                case '/': undo = '*'; break;
                default: undo = ' '; break;
            }
            return undo;
        }
    }

    // "Receiver": 

    class Calculator
    {
        private int curr = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': curr += operand; break;
                case '-': curr -= operand; break;
                case '*': curr *= operand; break;
                case '/': curr /= operand; break;
            }
            Console.WriteLine("Current value = {0,3} (following {1} {2})", curr, @operator, operand);
        }
    }

    // "Invoker":

    class User
    {
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();

        private int _current = 0;

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);

            // Do returning of operations
            for (int i = 0; i < levels; i++)
                if (_current < _commands.Count)
                    _commands[_current++].Execute();
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);

            // Do canceling of operations
            for (int i = 0; i < levels; i++)
                if (_current > 0)
                    _commands[--_current].UnExecute();
        }

        public void Compute(char @operator, int operand)
        {

            // Сreate command of operation and do it
            Command command = new CalculatorCommand(_calculator, @operator, operand);
            command.Execute();

            if (_current < _commands.Count)
            {
                // if "inside undo" we launch a new operation,
                // it is necessary to cut down the list of commands following after the current
                // else undo / redo will be incorrect
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            // Add operation to list of canceled
            _commands.Add(command);
            _current++;
        }
    }
}

    