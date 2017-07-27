using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Security;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Common
{
    internal static class ADP
    {
        static internal void CheckArgumentLength(string value, string parameterName)
        {
            CheckArgumentNull(value, parameterName);
            if (0 == value.Length)
            {
                throw Argument(GetString(Strings.ADP_EmptyString, parameterName)); // MDAC 94859
            }
        }

        static internal void CheckArgumentNull(object value, string parameterName)
        {
            if (null == value)
            {
                throw ArgumentNull(parameterName);
            }
        }

        static internal ArgumentException ConfigProviderNotFound()
        {
            return Argument(GetString(Strings.ConfigProviderNotFound));
        }

        static internal InvalidOperationException ConfigProviderInvalid()
        {
            return InvalidOperation(Strings.ConfigProviderInvalid);
        }

        static internal ConfigurationException ConfigProviderNotInstalled()
        {
            return Configuration(Strings.ConfigProviderNotInstalled);
        }

        static internal ConfigurationException ConfigProviderMissing()
        {
            return Configuration(Strings.ConfigProviderMissing);
        }

        static internal ConfigurationException ConfigUnrecognizedElement(XmlNode node)
        { // Res.Config_base_unrecognized_element
            return Configuration(GetString(Strings.ConfigUnrecognizedElement), node);
        }

        static internal ConfigurationException ConfigSectionsUnique(string sectionName)
        { // Res.Res.ConfigSectionsUnique
            return Configuration(GetString(Strings.ConfigSectionsUnique, sectionName));
        }

        static internal ConfigurationException ConfigBaseNoChildNodes(XmlNode node)
        { // Res.Config_base_no_child_nodes
            return Configuration(GetString(Strings.ConfigBaseNoChildNodes), node);
        }
        static internal ConfigurationException ConfigBaseElementsOnly(XmlNode node)
        { // Res.Config_base_elements_only
            return Configuration(GetString(Strings.ConfigBaseElementsOnly), node);
        }
        static internal ConfigurationException ConfigUnrecognizedAttributes(XmlNode node)
        { // Res.Config_base_unrecognized_attribute
            return Configuration(GetString(Strings.ConfigUnrecognizedAttributes, node.Attributes[0].Name), node);
        }
        static internal ConfigurationException ConfigRequiredAttributeMissing(string name, XmlNode node)
        { // Res.Config_base_required_attribute_missing
            return Configuration(GetString(Strings.ConfigRequiredAttributeMissing, name), node);
        }
        static internal ConfigurationException ConfigRequiredAttributeEmpty(string name, XmlNode node)
        { // Res.Config_base_required_attribute_empty
            return Configuration(GetString(Strings.ConfigRequiredAttributeEmpty, name), node);
        }

        #region Helpers

        // only StackOverflowException & ThreadAbortException are sealed classes
        static private readonly Type StackOverflowType = typeof(StackOverflowException);
        static private readonly Type OutOfMemoryType = typeof(OutOfMemoryException);
        static private readonly Type ThreadAbortType = typeof(ThreadAbortException);
        static private readonly Type NullReferenceType = typeof(NullReferenceException);
        static private readonly Type AccessViolationType = typeof(AccessViolationException);
        static private readonly Type SecurityType = typeof(SecurityException);

        static internal bool IsEmpty(string str)
        {
            return ((null == str) || (0 == str.Length));
        }

        static internal ArgumentException Argument(string error)
        {
            ArgumentException e = new ArgumentException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        static internal ArgumentNullException ArgumentNull(string parameter)
        {
            ArgumentNullException e = new ArgumentNullException(parameter);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        static internal ConfigurationException Configuration(string message)
        {
            ConfigurationException e = new ConfigurationErrorsException(message);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        static internal ConfigurationException Configuration(string message, XmlNode node)
        {
            ConfigurationException e = new ConfigurationErrorsException(message, node);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        static internal InvalidOperationException InvalidOperation(string error)
        {
            InvalidOperationException e = new InvalidOperationException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        static internal bool IsCatchableExceptionType(Exception e)
        {
            // a 'catchable' exception is defined by what it is not.
            Debug.Assert(e != null, "Unexpected null exception!");
            Type type = e.GetType();

            return ((type != StackOverflowType) &&
                     (type != OutOfMemoryType) &&
                     (type != ThreadAbortType) &&
                     (type != NullReferenceType) &&
                     (type != AccessViolationType) &&
                     !SecurityType.IsAssignableFrom(type));
        }

        static private void TraceException(string trace, Exception e)
        {
            Debug.Assert(null != e, "TraceException: null Exception");
        }

        static internal void TraceExceptionAsReturnValue(Exception e)
        {
            TraceException("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
        }

        static internal void TraceExceptionForCapture(Exception e)
        {
            Debug.Assert(ADP.IsCatchableExceptionType(e), "Invalid exception type, should have been re-thrown!");
            TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
        }

        static internal void TraceExceptionWithoutRethrow(Exception e)
        {
            Debug.Assert(ADP.IsCatchableExceptionType(e), "Invalid exception type, should have been re-thrown!");
            TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
        }

        static internal string GetString(string resource, params string[] parameters)
        {
            return String.Format(resource, parameters);
        }

        #endregion
    }
}
