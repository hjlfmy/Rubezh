﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:2.0.50727.3053
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace Firesec.Indicator
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class LEDProperties {
        
        private string[] zoneField;
        
        private deviceType[] deviceField;
        
        private string typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("zone", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] zone {
            get {
                return this.zoneField;
            }
            set {
                this.zoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("device", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public deviceType[] device {
            get {
                return this.deviceField;
            }
            set {
                this.deviceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deviceType {
        
        private string uIDField;
        
        private string state1Field;
        
        private string state2Field;
        
        private string state3Field;
        
        private string state4Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state1 {
            get {
                return this.state1Field;
            }
            set {
                this.state1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state2 {
            get {
                return this.state2Field;
            }
            set {
                this.state2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state3 {
            get {
                return this.state3Field;
            }
            set {
                this.state3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state4 {
            get {
                return this.state4Field;
            }
            set {
                this.state4Field = value;
            }
        }
    }
}