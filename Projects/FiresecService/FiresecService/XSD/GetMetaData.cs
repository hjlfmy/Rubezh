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
namespace Firesec.Metadata
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class param
    {

        private string nameField;

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class config
    {

        private configClass[] classField;

        private configDrv[] drvField;

        private string clsidField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("class", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configClass[] @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("drv", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configDrv[] drv
        {
            get
            {
                return this.drvField;
            }
            set
            {
                this.drvField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string clsid
        {
            get
            {
                return this.clsidField;
            }
            set
            {
                this.clsidField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configClass
    {

        private configClassParent[] parentField;

        private param[] paramField;

        private string clsidField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("parent", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configClassParent[] parent
        {
            get
            {
                return this.parentField;
            }
            set
            {
                this.parentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("param")]
        public param[] param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string clsid
        {
            get
            {
                return this.clsidField;
            }
            set
            {
                this.clsidField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configClassParent
    {

        private string clsidField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string clsid
        {
            get
            {
                return this.clsidField;
            }
            set
            {
                this.clsidField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configDrv
    {

        private configDrvParamInfo[] paramInfoField;

        private configDrvPropInfo[] propInfoField;

        private configDrvState[] stateField;

        private string nameField;

        private string idField;

        private string clsidField;

        private string optionsField;

        private string maxZoneCardinalityField;

        private string minZoneCardinalityField;

        private string addrGroupField;

        private string parentInAddrField;

        private string shortNameField;

        private string acr_enabledField;

        private string acr_fromField;

        private string acr_toField;

        private string ar_enabledField;

        private string ar_fromField;

        private string ar_toField;

        private string ar_no_addrField;

        private string catField;

        private string caseCntField;

        private string childAddrMaskField;

        private string validCharsField;

        private string dev_iconField;

        private string addrMaskField;

        private string lim_parentField;

        private string altIntfField;

        private string id_aliasField;

        private string res_addrField;

        private string addrPrefixField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("paramInfo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configDrvParamInfo[] paramInfo
        {
            get
            {
                return this.paramInfoField;
            }
            set
            {
                this.paramInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("propInfo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configDrvPropInfo[] propInfo
        {
            get
            {
                return this.propInfoField;
            }
            set
            {
                this.propInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("state", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public configDrvState[] state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string clsid
        {
            get
            {
                return this.clsidField;
            }
            set
            {
                this.clsidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string options
        {
            get
            {
                return this.optionsField;
            }
            set
            {
                this.optionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxZoneCardinality
        {
            get
            {
                return this.maxZoneCardinalityField;
            }
            set
            {
                this.maxZoneCardinalityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string minZoneCardinality
        {
            get
            {
                return this.minZoneCardinalityField;
            }
            set
            {
                this.minZoneCardinalityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string addrGroup
        {
            get
            {
                return this.addrGroupField;
            }
            set
            {
                this.addrGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string parentInAddr
        {
            get
            {
                return this.parentInAddrField;
            }
            set
            {
                this.parentInAddrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string shortName
        {
            get
            {
                return this.shortNameField;
            }
            set
            {
                this.shortNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acr_enabled
        {
            get
            {
                return this.acr_enabledField;
            }
            set
            {
                this.acr_enabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acr_from
        {
            get
            {
                return this.acr_fromField;
            }
            set
            {
                this.acr_fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acr_to
        {
            get
            {
                return this.acr_toField;
            }
            set
            {
                this.acr_toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ar_enabled
        {
            get
            {
                return this.ar_enabledField;
            }
            set
            {
                this.ar_enabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ar_from
        {
            get
            {
                return this.ar_fromField;
            }
            set
            {
                this.ar_fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ar_to
        {
            get
            {
                return this.ar_toField;
            }
            set
            {
                this.ar_toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ar_no_addr
        {
            get
            {
                return this.ar_no_addrField;
            }
            set
            {
                this.ar_no_addrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cat
        {
            get
            {
                return this.catField;
            }
            set
            {
                this.catField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string caseCnt
        {
            get
            {
                return this.caseCntField;
            }
            set
            {
                this.caseCntField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string childAddrMask
        {
            get
            {
                return this.childAddrMaskField;
            }
            set
            {
                this.childAddrMaskField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string validChars
        {
            get
            {
                return this.validCharsField;
            }
            set
            {
                this.validCharsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dev_icon
        {
            get
            {
                return this.dev_iconField;
            }
            set
            {
                this.dev_iconField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string addrMask
        {
            get
            {
                return this.addrMaskField;
            }
            set
            {
                this.addrMaskField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lim_parent
        {
            get
            {
                return this.lim_parentField;
            }
            set
            {
                this.lim_parentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string altIntf
        {
            get
            {
                return this.altIntfField;
            }
            set
            {
                this.altIntfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id_alias
        {
            get
            {
                return this.id_aliasField;
            }
            set
            {
                this.id_aliasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string res_addr
        {
            get
            {
                return this.res_addrField;
            }
            set
            {
                this.res_addrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string addrPrefix
        {
            get
            {
                return this.addrPrefixField;
            }
            set
            {
                this.addrPrefixField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configDrvParamInfo
    {

        private string nameField;

        private string typeField;

        private string defaultField;

        private string editTypeField;

        private string captionField;

        private string helpIndexField;

        private string hintField;

        private string paramIDField;

        private string minField;

        private string maxField;

        private string tslenField;

        private string hiddenField;

        private string showOnlyInStateField;

        private string nameSourceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string editType
        {
            get
            {
                return this.editTypeField;
            }
            set
            {
                this.editTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string caption
        {
            get
            {
                return this.captionField;
            }
            set
            {
                this.captionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string helpIndex
        {
            get
            {
                return this.helpIndexField;
            }
            set
            {
                this.helpIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hint
        {
            get
            {
                return this.hintField;
            }
            set
            {
                this.hintField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string paramID
        {
            get
            {
                return this.paramIDField;
            }
            set
            {
                this.paramIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tslen
        {
            get
            {
                return this.tslenField;
            }
            set
            {
                this.tslenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hidden
        {
            get
            {
                return this.hiddenField;
            }
            set
            {
                this.hiddenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string showOnlyInState
        {
            get
            {
                return this.showOnlyInStateField;
            }
            set
            {
                this.showOnlyInStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nameSource
        {
            get
            {
                return this.nameSourceField;
            }
            set
            {
                this.nameSourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configDrvPropInfo
    {

        private param[] paramField;

        private string nameField;

        private string typeField;

        private string defaultField;

        private string editTypeField;

        private string captionField;

        private string helpIndexField;

        private string hintField;

        private string paramIDField;

        private string minField;

        private string maxField;

        private string tslenField;

        private string hiddenField;

        private string showOnlyInStateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("param")]
        public param[] param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string editType
        {
            get
            {
                return this.editTypeField;
            }
            set
            {
                this.editTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string caption
        {
            get
            {
                return this.captionField;
            }
            set
            {
                this.captionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string helpIndex
        {
            get
            {
                return this.helpIndexField;
            }
            set
            {
                this.helpIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hint
        {
            get
            {
                return this.hintField;
            }
            set
            {
                this.hintField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string paramID
        {
            get
            {
                return this.paramIDField;
            }
            set
            {
                this.paramIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tslen
        {
            get
            {
                return this.tslenField;
            }
            set
            {
                this.tslenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hidden
        {
            get
            {
                return this.hiddenField;
            }
            set
            {
                this.hiddenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string showOnlyInState
        {
            get
            {
                return this.showOnlyInStateField;
            }
            set
            {
                this.showOnlyInStateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configDrvState
    {

        private string idField;

        private string nameField;

        private string codeField;

        private string classField;

        private string typeField;

        private string canResetOnPanelField;

        private string affectedParentField;

        private string affectChildrenField;

        private string manualResetField;

        private string primaryStateField;

        private string nameSourceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CanResetOnPanel
        {
            get
            {
                return this.canResetOnPanelField;
            }
            set
            {
                this.canResetOnPanelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AffectedParent
        {
            get
            {
                return this.affectedParentField;
            }
            set
            {
                this.affectedParentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string affectChildren
        {
            get
            {
                return this.affectChildrenField;
            }
            set
            {
                this.affectChildrenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string manualReset
        {
            get
            {
                return this.manualResetField;
            }
            set
            {
                this.manualResetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string primaryState
        {
            get
            {
                return this.primaryStateField;
            }
            set
            {
                this.primaryStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nameSource
        {
            get
            {
                return this.nameSourceField;
            }
            set
            {
                this.nameSourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("config", typeof(config))]
        [System.Xml.Serialization.XmlElementAttribute("param", typeof(param))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}