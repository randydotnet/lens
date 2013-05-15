﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lens.SyntaxTree.Translations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CompilerMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CompilerMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lens.SyntaxTree.Translations.CompilerMessages", typeof(CompilerMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно использовать выражения типа &apos;{0}&apos; в качестве значения параметра типа &apos;{1}&apos;!.
        /// </summary>
        public static string ArgumentTypeMismatch {
            get {
                return ResourceManager.GetString("ArgumentTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно добавить объект типа &apos;{0}&apos; в массив типа &apos;{1}&apos;!.
        /// </summary>
        public static string ArrayElementTypeMismatch {
            get {
                return ResourceManager.GetString("ArrayElementTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Инициализатор массива должен содержать хотя бы один элемент! Для создания пустых массивов воспользуйтесь явным конструктором..
        /// </summary>
        public static string ArrayEmpty {
            get {
                return ResourceManager.GetString("ArrayEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип массива не может быть выведен! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string ArrayTypeUnknown {
            get {
                return ResourceManager.GetString("ArrayTypeUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно создать делегат из метода, принимающего более 16 аргументов!.
        /// </summary>
        public static string CallableTooManyArguments {
            get {
                return ResourceManager.GetString("CallableTooManyArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to У делегатов &apos;{0}&apos; и &apos;{1}&apos; не совпадают аргументы!.
        /// </summary>
        public static string CastDelegateArgTypesMismatch {
            get {
                return ResourceManager.GetString("CastDelegateArgTypesMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to У делегатов &apos;{0}&apos; и &apos;{1}&apos; не совпадают возвращаемые значения!.
        /// </summary>
        public static string CastDelegateReturnTypesMismatch {
            get {
                return ResourceManager.GetString("CastDelegateReturnTypesMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно привести выражение null к типу &apos;{0}&apos;, поскольку он передается по значению!.
        /// </summary>
        public static string CastNullValueType {
            get {
                return ResourceManager.GetString("CastNullValueType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно привести объект типа &apos;{0}&apos; к типу &apos;{1}&apos;!.
        /// </summary>
        public static string CastTypesMismatch {
            get {
                return ResourceManager.GetString("CastTypesMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выражение catch недостижимо, поскольку предыдущее перехватывает все типы исключений!.
        /// </summary>
        public static string CatchClauseUnreachable {
            get {
                return ResourceManager.GetString("CatchClauseUnreachable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; уже использован в выражении catch текущего блока try!.
        /// </summary>
        public static string CatchTypeDuplicate {
            get {
                return ResourceManager.GetString("CatchTypeDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не может быть использован в выражении catch, поскольку он не наследуется от System.Exception!.
        /// </summary>
        public static string CatchTypeNotException {
            get {
                return ResourceManager.GetString("CatchTypeNotException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно использовать неявную переменную в замыкании!.
        /// </summary>
        public static string ClosureImplicit {
            get {
                return ResourceManager.GetString("ClosureImplicit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно использовать аргумент, передаваемый по ссылке, в замыкании!.
        /// </summary>
        public static string ClosureRef {
            get {
                return ResourceManager.GetString("ClosureRef", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Блок кода должен содержать хотя бы одно выражение!.
        /// </summary>
        public static string CodeBlockEmpty {
            get {
                return ResourceManager.GetString("CodeBlockEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Объявление переменной или константы не может быть последним выражением в блоке!.
        /// </summary>
        public static string CodeBlockLastVar {
            get {
                return ResourceManager.GetString("CodeBlockLastVar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно определить общий тип для ветвей условия! Типы выражений -  &apos;{0}&apos; и &apos;{1}&apos;..
        /// </summary>
        public static string ConditionInconsistentTyping {
            get {
                return ResourceManager.GetString("ConditionInconsistentTyping", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Константы нельзя передавать в функции по ссылке!.
        /// </summary>
        public static string ConstantByRef {
            get {
                return ResourceManager.GetString("ConstantByRef", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Деление на ноль!.
        /// </summary>
        public static string ConstantDivisionByZero {
            get {
                return ResourceManager.GetString("ConstantDivisionByZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Конструктор не может быть статическим!.
        /// </summary>
        public static string ConstructorStatic {
            get {
                return ResourceManager.GetString("ConstructorStatic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Контекст #{0} не существует!.
        /// </summary>
        public static string ContextNotFound {
            get {
                return ResourceManager.GetString("ContextNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Контекст #{0} был освобожден!.
        /// </summary>
        public static string ContextUnregistered {
            get {
                return ResourceManager.GetString("ContextUnregistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вызов делегата типа &apos;{0}&apos; требует {1} аргументов, а передано {2}!.
        /// </summary>
        public static string DelegateArgumentsCountMismatch {
            get {
                return ResourceManager.GetString("DelegateArgumentsCountMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Инициализатор словаря должен содержать хотя бы один элемент! Для создания пустых словарей воспользуйтесь явным конструктором..
        /// </summary>
        public static string DictionaryEmpty {
            get {
                return ResourceManager.GetString("DictionaryEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно добавить ключ типа &apos;{0}&apos; в Dictionary&lt;{1}, {2}&gt;!.
        /// </summary>
        public static string DictionaryKeyTypeMismatch {
            get {
                return ResourceManager.GetString("DictionaryKeyTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип словаря не может быть выведен! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string DictionaryTypeUnknown {
            get {
                return ResourceManager.GetString("DictionaryTypeUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно добавить значение типа &apos;{0}&apos; в Dictionary&lt;{1}, {2}&gt;!.
        /// </summary>
        public static string DictionaryValueTypeMismatch {
            get {
                return ResourceManager.GetString("DictionaryValueTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно получить доступ к &apos;{0}&apos; из статического контекста!.
        /// </summary>
        public static string DynamicMemberFromStaticContext {
            get {
                return ResourceManager.GetString("DynamicMemberFromStaticContext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип выражения не может быть выведен! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string ExpressionNull {
            get {
                return ResourceManager.GetString("ExpressionNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Требуется выражение, возвращающее значение!.
        /// </summary>
        public static string ExpressionVoid {
            get {
                return ResourceManager.GetString("ExpressionVoid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Найдено несколько подходящих переопределений функции &apos;{0}&apos;! Воспользуйтесь операцией приведения типов для указания конкретного переопределения!.
        /// </summary>
        public static string FunctionInvocationAmbiguous {
            get {
                return ResourceManager.GetString("FunctionInvocationAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не найдено ни одного переопределения функции &apos;{0}&apos;, принимающей переданные аргументы!.
        /// </summary>
        public static string FunctionNotFound {
            get {
                return ResourceManager.GetString("FunctionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Количество обобщенных аргументов не совпадает!.
        /// </summary>
        public static string GenericArgCountMismatch {
            get {
                return ResourceManager.GetString("GenericArgCountMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для обобщенного аргумента &apos;{0}&apos; переданы несовпадающие значения: &apos;{1}&apos; и &apos;{2}&apos;!.
        /// </summary>
        public static string GenericArgMismatch {
            get {
                return ResourceManager.GetString("GenericArgMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Метод &apos;{0}&apos; не принимает обобщенных аргументов!.
        /// </summary>
        public static string GenericArgsToNonGenericMethod {
            get {
                return ResourceManager.GetString("GenericArgsToNonGenericMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение обобщенного аргумента &apos;{0}&apos; не может быть вычислено!.
        /// </summary>
        public static string GenericArgumentNotResolved {
            get {
                return ResourceManager.GetString("GenericArgumentNotResolved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для обобщенного аргумента &apos;{0}&apos; был явно указан тип &apos;{1}&apos;, но использование подразумевает тип &apos;{2}&apos;!.
        /// </summary>
        public static string GenericHintMismatch {
            get {
                return ResourceManager.GetString("GenericHintMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно вычислить значение обобщенного аргумента &apos;{0}&apos;, используя тип &apos;{1}&apos;!.
        /// </summary>
        public static string GenericImplementationWrongType {
            get {
                return ResourceManager.GetString("GenericImplementationWrongType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно определить обобщенные аргументы интерфейса &apos;{0}&apos;: тип &apos;{1}&apos; реализует несколько его переопределений!.
        /// </summary>
        public static string GenericInterfaceMultipleImplementations {
            get {
                return ResourceManager.GetString("GenericInterfaceMultipleImplementations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не реализует интерфейс &apos;{1}&apos;!.
        /// </summary>
        public static string GenericInterfaceNotImplemented {
            get {
                return ResourceManager.GetString("GenericInterfaceNotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Обобщенный аргумент &apos;{0}&apos; не был найден!.
        /// </summary>
        public static string GenericParameterNotFound {
            get {
                return ResourceManager.GetString("GenericParameterNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно получить значение глобального свойства &apos;{0}&apos;!.
        /// </summary>
        public static string GlobalPropertyNoGetter {
            get {
                return ResourceManager.GetString("GlobalPropertyNoGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно задать значение глобального свойства &apos;{0}&apos;!.
        /// </summary>
        public static string GlobalPropertyNoSetter {
            get {
                return ResourceManager.GetString("GlobalPropertyNoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно присвоить глобальному свойству &apos;{0}&apos; значение типа &apos;{1}&apos;! Возможно, требуется явное приведение типа..
        /// </summary>
        public static string GlobalPropertyTypeMismatch {
            get {
                return ResourceManager.GetString("GlobalPropertyTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; является константой и изменение ее значения запрещено!.
        /// </summary>
        public static string IdentifierIsConstant {
            get {
                return ResourceManager.GetString("IdentifierIsConstant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Переменная или функция без параметров с названием &apos;{0}&apos; не найдена!.
        /// </summary>
        public static string IdentifierNotFound {
            get {
                return ResourceManager.GetString("IdentifierNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно присвоить переменной &apos;{0}&apos; значение типа &apos;{1}&apos;! Возможно. требуется явное приведение типа..
        /// </summary>
        public static string IdentifierTypeMismatch {
            get {
                return ResourceManager.GetString("IdentifierTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно неявно привести выражение типа &apos;{0}&apos; к типу &apos;{1}&apos;!.
        /// </summary>
        public static string ImplicitCastImpossible {
            get {
                return ResourceManager.GetString("ImplicitCastImpossible", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сохраняемые сборки не поддерживают импорт!.
        /// </summary>
        public static string ImportIntoSaveableAssembly {
            get {
                return ResourceManager.GetString("ImportIntoSaveableAssembly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Только публичные, статические и не содержащие обобщенных аргументов методы могут быть импортированы!.
        /// </summary>
        public static string ImportUnsupportedMethod {
            get {
                return ResourceManager.GetString("ImportUnsupportedMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Индексатор не может быть однозначно определен! Подходят как минимум два переопределения:{3}{0}[{1}]{3}{0}[{2}].
        /// </summary>
        public static string IndexAmbigious {
            get {
                return ResourceManager.GetString("IndexAmbigious", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит индексатора на чтение, который бы принимал индекс типа &apos;{1}&apos;!.
        /// </summary>
        public static string IndexGetterNotFound {
            get {
                return ResourceManager.GetString("IndexGetterNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит индексатора на запись, который бы принимал индекс типа &apos;{1}&apos;!.
        /// </summary>
        public static string IndexSetterNotFound {
            get {
                return ResourceManager.GetString("IndexSetterNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Возвращаемый тип анонимной функции не может быть выведен! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string LambdaReturnTypeUnknown {
            get {
                return ResourceManager.GetString("LambdaReturnTypeUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно добавить объект типа &apos;{0}&apos; в List&lt;{1}&gt;!.
        /// </summary>
        public static string ListElementTypeMismatch {
            get {
                return ResourceManager.GetString("ListElementTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Инициализатор списка должен содержать хотя бы один элемент! Для создания пустых списков воспользуйтесь явным конструктором..
        /// </summary>
        public static string ListEmpty {
            get {
                return ResourceManager.GetString("ListEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип списка не может быть выведен! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string ListTypeUnknown {
            get {
                return ResourceManager.GetString("ListTypeUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно применить оператор &apos;{0}&apos; к аргументам типов &apos;{1}&apos; и &apos;{2}&apos;!.
        /// </summary>
        public static string OperatorBinaryTypesMismatch {
            get {
                return ResourceManager.GetString("OperatorBinaryTypesMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно провести математическую операцию к числам разной знаковости!.
        /// </summary>
        public static string OperatorTypesSignednessMismatch {
            get {
                return ResourceManager.GetString("OperatorTypesSignednessMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно применить оператор &apos;{0}&apos; к аргументу типа &apos;{1}&apos;!.
        /// </summary>
        public static string OperatorUnaryTypeMismatch {
            get {
                return ResourceManager.GetString("OperatorUnaryTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для вызова конструктора без параметров требуется указать пустые скобки!.
        /// </summary>
        public static string ParameterlessConstructorParens {
            get {
                return ResourceManager.GetString("ParameterlessConstructorParens", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение свойства #{0} не может быть получено!.
        /// </summary>
        public static string PropertyIdNoGetter {
            get {
                return ResourceManager.GetString("PropertyIdNoGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение свойства #{0} не может быть задано!.
        /// </summary>
        public static string PropertyIdNoSetter {
            get {
                return ResourceManager.GetString("PropertyIdNoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Свойство #{0} не найдено!.
        /// </summary>
        public static string PropertyIdNotFound {
            get {
                return ResourceManager.GetString("PropertyIdNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Свойство &apos;{0}&apos; уже было импортировано!.
        /// </summary>
        public static string PropertyImported {
            get {
                return ResourceManager.GetString("PropertyImported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение свойства &apos;{0}.{1}&apos; не может быть получено!.
        /// </summary>
        public static string PropertyNoGetter {
            get {
                return ResourceManager.GetString("PropertyNoGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение свойства &apos;{0}.{1}&apos; не может быть pflfyj!.
        /// </summary>
        public static string PropertyNoSetter {
            get {
                return ResourceManager.GetString("PropertyNoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Функция типа &apos;{0}&apos; не может вернуть объект типа &apos;{1}&apos;!.
        /// </summary>
        public static string ReturnTypeMismatch {
            get {
                return ResourceManager.GetString("ReturnTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Требуется выражение! Повторный выброс исключения допустим только в выражении catch..
        /// </summary>
        public static string ThrowArgumentExpected {
            get {
                return ResourceManager.GetString("ThrowArgumentExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не может быть использован в выражении throw, поскольку он не наследуется от System.Exception!.
        /// </summary>
        public static string ThrowTypeNotException {
            get {
                return ResourceManager.GetString("ThrowTypeNotException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кортеж должен содержать хотя бы один объект!.
        /// </summary>
        public static string TupleNoArgs {
            get {
                return ResourceManager.GetString("TupleNoArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кортеж не может содержать больше 8 объектов! Воспользуйтесь структурой или вложенным кортежом..
        /// </summary>
        public static string TupleTooManyArgs {
            get {
                return ResourceManager.GetString("TupleTooManyArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно создать экземпляр абстрактного типа &apos;{0}&apos;!.
        /// </summary>
        public static string TypeAbstract {
            get {
                return ResourceManager.GetString("TypeAbstract", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно применить обобщенные параметры к &apos;{0}.{1}&apos;, потому что это не метод!.
        /// </summary>
        public static string TypeArgumentsForNonMethod {
            get {
                return ResourceManager.GetString("TypeArgumentsForNonMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; содержит несколько подходящих конструкторов! Воспользуйтесь операцией приведения типов ..
        /// </summary>
        public static string TypeConstructorAmbiguos {
            get {
                return ResourceManager.GetString("TypeConstructorAmbiguos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит конструктор с подходящим списком аргументов!.
        /// </summary>
        public static string TypeConstructorNotFound {
            get {
                return ResourceManager.GetString("TypeConstructorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; уже объявлен!.
        /// </summary>
        public static string TypeDefined {
            get {
                return ResourceManager.GetString("TypeDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит свойства, метода или поля под названием &apos;{1}&apos;!.
        /// </summary>
        public static string TypeIdentifierNotFound {
            get {
                return ResourceManager.GetString("TypeIdentifierNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неоднозначное указание типа: тип &apos;{0}&apos; найден как минимум в двух пространствах имен:{5}{1} в сборке {2}{5}{3} в сборке {4}.
        /// </summary>
        public static string TypeIsAmbiguous {
            get {
                return ResourceManager.GetString("TypeIsAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; содержит несколько подходящих переопределений метода &apos;{1}&apos;! Укажите типы аргументов явно..
        /// </summary>
        public static string TypeMethodAmbiguous {
            get {
                return ResourceManager.GetString("TypeMethodAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; содержит несколько подходящих переопределений метода &apos;{1}&apos;! Воспользуйтесь операцией приведения типов..
        /// </summary>
        public static string TypeMethodInvocationAmbiguous {
            get {
                return ResourceManager.GetString("TypeMethodInvocationAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит метод &apos;{1}&apos; с подходящим списков аргументов! Соответствующий метод-расширение также не найден..
        /// </summary>
        public static string TypeMethodNotFound {
            get {
                return ResourceManager.GetString("TypeMethodNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не является делегатом!.
        /// </summary>
        public static string TypeNotCallable {
            get {
                return ResourceManager.GetString("TypeNotCallable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не найден!.
        /// </summary>
        public static string TypeNotFound {
            get {
                return ResourceManager.GetString("TypeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; нельзя использовать в булевом контексте!.
        /// </summary>
        public static string TypeNotImplicitlyBoolean {
            get {
                return ResourceManager.GetString("TypeNotImplicitlyBoolean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не является числовым!.
        /// </summary>
        public static string TypeNotNumeric {
            get {
                return ResourceManager.GetString("TypeNotNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тип &apos;{0}&apos; не содержит поля или свойства &apos;{1}&apos;!.
        /// </summary>
        public static string TypeSettableIdentifierNotFound {
            get {
                return ResourceManager.GetString("TypeSettableIdentifierNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно сравнить объекты типов &apos;{0}&apos; и &apos;{1}&apos;!.
        /// </summary>
        public static string TypesIncomparable {
            get {
                return ResourceManager.GetString("TypesIncomparable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Переменная &apos;{0}&apos; уже объявлена!.
        /// </summary>
        public static string VariableDefined {
            get {
                return ResourceManager.GetString("VariableDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Переменная &apos;{0}&apos; не объявлена в данной области видимости!.
        /// </summary>
        public static string VariableNotFound {
            get {
                return ResourceManager.GetString("VariableNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно создать экземпляр типа Unit или void!.
        /// </summary>
        public static string VoidTypeDefault {
            get {
                return ResourceManager.GetString("VoidTypeDefault", resourceCulture);
            }
        }
    }
}
