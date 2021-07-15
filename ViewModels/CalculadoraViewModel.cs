using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using WPF_Calculadora.Models;
using WPF_Calculadora.Core;

namespace WPF_Calculadora.ViewModels
{
    public class CalculadoraViewModel : ViewModelBase
    {
        #region PROPIEDADES
        private Operation current__Operation;
        private MathOperationCollection history__Operations;

        public Operation Current__Operation
        {
            get { return current__Operation; }
            set
            {
                current__Operation = value;
                current__Operation.Expresion = value.Expresion; //Para obtener el resultado automaticamente
                OnPropertyChanged("Current__Operation");
            }
        }
        public MathOperationCollection History__Operations
        {
            get { return history__Operations; }
            set {
                history__Operations = value;
                Modificar__CurrentOperation();
                OnPropertyChanged("History__Operations");
            }
        }
        #endregion

        #region COMANDOS

        private ICommand reiniciar_Command;
        public ICommand Reiniciar_Command
        {
            get
            {
                if (reiniciar_Command == null)
                {
                    reiniciar_Command = new RelayCommand(new Action(Reiniciar_Historial_De_Operaciones));
                }
                return reiniciar_Command;
            }
        }
        private ICommand eliminar_Command;
        public ICommand Eliminar_Command
        {
            get
            {
                if (eliminar_Command == null)
                {
                    eliminar_Command = new RelayCommand(new Action(Eliminar_Del_Historial_De_Operaciones), () => PuedoEliminarElElemento());
                }
                return eliminar_Command;
            }
        }
        private ICommand nuevo_Command;
        public ICommand Nuevo_Command
        {
            get
            {
                if (nuevo_Command == null)
                {
                    nuevo_Command = new RelayCommand(new Action(Nuevo_Elemento_Para_Agregar_Al_Historial));
                }
                return nuevo_Command;
            }
        }
        private ICommand agregar_Command;
        public ICommand Agregar_Command
        {
            get
            {
                if (agregar_Command == null)
                {
                    agregar_Command = new RelayCommand(new Action(Agregar_Al_Historial_De_Operaciones), () => PuedoAgregarElElemento());
                }
                return agregar_Command;
            }
        }
        private ICommand resolver_Command;

        public ICommand Resolver_Command
        {
            get
            {
                if (resolver_Command == null)
                {
                    resolver_Command = new RelayCommand(new Action (Resolver_Operacion_Actual));
                }
                return resolver_Command;
            }
        }
        #endregion

        #region METODOS
        private void Reiniciar_Historial_De_Operaciones()
        {
            History__Operations = new MathOperationCollection();
            current__Operation = new Operation(getRandomName(), "0");
        }
        private void Eliminar_Del_Historial_De_Operaciones()
        {
            History__Operations.Remove(current__Operation);
            Modificar__CurrentOperation();
        }
        private void Agregar_Al_Historial_De_Operaciones()
        {
            Operation operacionResuelta = new Operation(Current__Operation.Nombre, Current__Operation.Expresion);
            History__Operations.Add(operacionResuelta);
            Modificar__CurrentOperation();
        }
        private void Nuevo_Elemento_Para_Agregar_Al_Historial()
        {
            Current__Operation = new Operation(getRandomName(), "0");
        }
        private void Resolver_Operacion_Actual()
        {
            int indiceEnHistorial = GetIndexOfOperation(Current__Operation);
            Operation operacionActualizada = new Operation(Current__Operation.Nombre, Current__Operation.Expresion);
            if (indiceEnHistorial >= 0)
            {   //ACTUALIZA EL RESULTADO SI YA EXISTE EN EL HISTORIAL
                History__Operations.Insert(indiceEnHistorial, operacionActualizada);
                History__Operations.Remove(Current__Operation);
            }
            Current__Operation = operacionActualizada;
        }
        #endregion
        #region DEPENDENCIAS DE LOS METODOS
        private void Modificar__CurrentOperation()
        {
            if (History__Operations != null && History__Operations.Count > 0)
            {
                //COLOCA EL COMO ELEMENTO SELECCIONADO EL ULTIMO ELEMENTO
                current__Operation = History__Operations[History__Operations.Count - 1];
            }
            else
            {
                //REFERENCIA A UN ELEMENTO NULO O VACIO
                current__Operation = new Operation(getRandomName(), "0");
            }
            //ACTUALIZA LOS VALORES MOSTRADOS EN PANTALLA
            OnPropertyChanged("Current__Operation");
        }
        #endregion
        #region CONDICION PARA HABILITAR O DESHABILITAR BOTONES
        private bool PuedoEliminarElElemento()
        {
            return (GetIndexOfOperation(current__Operation) >= 0);
        }
        private bool PuedoAgregarElElemento()
        {
            return (GetIndexOfOperation(current__Operation) < 0);
        }
        #endregion
        #region FUNCIONES DE LA LISTA
        private int GetIndexOfOperation(Operation searchOperation)
        {
            int indexOfOperation = -1;
            for (int i = 0; i < History__Operations.Count; i++)
            {
                if (History__Operations[i].Nombre.Equals(searchOperation.Nombre))
                {
                    indexOfOperation = i;
                    Operation operationTemp = History__Operations[i];
                    break;
                }
            }
            return indexOfOperation;
        }
        public string getRandomName()
        {   //GENERA VALORES STRING
            return (Guid.NewGuid().ToString().Substring(0, 3));
        }
        public CalculadoraViewModel()
        {
            History__Operations = new MathOperationCollection();
        }
        #endregion
    }
}
