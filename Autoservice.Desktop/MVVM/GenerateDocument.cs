using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Desktop.MVVM
{
    class GenerateDocument : Command
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        public GenerateDocument(Action<object> execute, Predicate<object> canExecute = null) {
            _execute = execute;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
