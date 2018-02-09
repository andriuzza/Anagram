
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class RS
{

    private RSHeader headerField;

    private RSArticleHierarchyData articleHierarchyDataField;

    /// <remarks/>
    public RSHeader Header
    {
        get
        {
            return this.headerField;
        }
        set
        {
            this.headerField = value;
        }
    }

    /// <remarks/>
    public RSArticleHierarchyData ArticleHierarchyData
    {
        get
        {
            return this.articleHierarchyDataField;
        }
        set
        {
            this.articleHierarchyDataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSHeader
{

    private RSHeaderSource sourceField;

    private RSHeaderMessage messageField;

    /// <remarks/>
    public RSHeaderSource Source
    {
        get
        {
            return this.sourceField;
        }
        set
        {
            this.sourceField = value;
        }
    }

    /// <remarks/>
    public RSHeaderMessage Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSHeaderSource
{

    private string nameField;

    /// <remarks/>
    public string Name
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSHeaderMessage
{

    private string conversationIdField;

    private decimal versionField;

    private System.DateTime createdDateField;

    private System.DateTime sentDateField;

    private byte isPartialField;

    private byte isCompleteField;

    private string contentTypeField;

    /// <remarks/>
    public string ConversationId
    {
        get
        {
            return this.conversationIdField;
        }
        set
        {
            this.conversationIdField = value;
        }
    }

    /// <remarks/>
    public decimal Version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    public System.DateTime CreatedDate
    {
        get
        {
            return this.createdDateField;
        }
        set
        {
            this.createdDateField = value;
        }
    }

    /// <remarks/>
    public System.DateTime SentDate
    {
        get
        {
            return this.sentDateField;
        }
        set
        {
            this.sentDateField = value;
        }
    }

    /// <remarks/>
    public byte IsPartial
    {
        get
        {
            return this.isPartialField;
        }
        set
        {
            this.isPartialField = value;
        }
    }

    /// <remarks/>
    public byte IsComplete
    {
        get
        {
            return this.isCompleteField;
        }
        set
        {
            this.isCompleteField = value;
        }
    }

    /// <remarks/>
    public string ContentType
    {
        get
        {
            return this.contentTypeField;
        }
        set
        {
            this.contentTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSArticleHierarchyData
{

    private RSArticleHierarchyDataArticleHierarchyLevel[] articleHierarchyLevelsField;

    private RSArticleHierarchyDataArticleHierarchy[] articleHierarchiesField;

    private RSArticleHierarchyDataArticleHierarchyExtraInfos articleHierarchyExtraInfosField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ArticleHierarchyLevel", IsNullable = false)]
    public RSArticleHierarchyDataArticleHierarchyLevel[] ArticleHierarchyLevels
    {
        get
        {
            return this.articleHierarchyLevelsField;
        }
        set
        {
            this.articleHierarchyLevelsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ArticleHierarchy", IsNullable = false)]
    public RSArticleHierarchyDataArticleHierarchy[] ArticleHierarchies
    {
        get
        {
            return this.articleHierarchiesField;
        }
        set
        {
            this.articleHierarchiesField = value;
        }
    }

    /// <remarks/>
    public RSArticleHierarchyDataArticleHierarchyExtraInfos ArticleHierarchyExtraInfos
    {
        get
        {
            return this.articleHierarchyExtraInfosField;
        }
        set
        {
            this.articleHierarchyExtraInfosField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSArticleHierarchyDataArticleHierarchyLevel
{

    private byte articleHierarchyLevelNoField;

    private string articleHierarchyLevelNameField;

    private byte articleHierarchyLevelField;

    private byte articleHierarchyLevelStatusNoField;

    private System.DateTime deletedDateField;

    /// <remarks/>
    public byte ArticleHierarchyLevelNo
    {
        get
        {
            return this.articleHierarchyLevelNoField;
        }
        set
        {
            this.articleHierarchyLevelNoField = value;
        }
    }

    /// <remarks/>
    public string ArticleHierarchyLevelName
    {
        get
        {
            return this.articleHierarchyLevelNameField;
        }
        set
        {
            this.articleHierarchyLevelNameField = value;
        }
    }

    /// <remarks/>
    public byte ArticleHierarchyLevel
    {
        get
        {
            return this.articleHierarchyLevelField;
        }
        set
        {
            this.articleHierarchyLevelField = value;
        }
    }

    /// <remarks/>
    public byte ArticleHierarchyLevelStatusNo
    {
        get
        {
            return this.articleHierarchyLevelStatusNoField;
        }
        set
        {
            this.articleHierarchyLevelStatusNoField = value;
        }
    }

    /// <remarks/>
    public System.DateTime DeletedDate
    {
        get
        {
            return this.deletedDateField;
        }
        set
        {
            this.deletedDateField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSArticleHierarchyDataArticleHierarchy
{

    private string articleHierarchyIdField;

    private string articleHierarchyDisplayIdField;

    private string articleHierarchyNameField;

    private uint parentArticleHierarchyIdField;

    private bool parentArticleHierarchyIdFieldSpecified;

    private byte articleHierarchyLevelNoField;

    private byte articleHierarchyStatusNoField;

    private decimal defaultMarkupPercentageField;

    private bool defaultMarkupPercentageFieldSpecified;

    private System.DateTime modifiedDateField;

    /// <remarks/>
    public string ArticleHierarchyId
    {
        get
        {
            return this.articleHierarchyIdField;
        }
        set
        {
            this.articleHierarchyIdField = value;
        }
    }

    /// <remarks/>
    public string ArticleHierarchyDisplayId
    {
        get
        {
            return this.articleHierarchyDisplayIdField;
        }
        set
        {
            this.articleHierarchyDisplayIdField = value;
        }
    }

    /// <remarks/>
    public string ArticleHierarchyName
    {
        get
        {
            return this.articleHierarchyNameField;
        }
        set
        {
            this.articleHierarchyNameField = value;
        }
    }

    /// <remarks/>
    public uint ParentArticleHierarchyId
    {
        get
        {
            return this.parentArticleHierarchyIdField;
        }
        set
        {
            this.parentArticleHierarchyIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ParentArticleHierarchyIdSpecified
    {
        get
        {
            return this.parentArticleHierarchyIdFieldSpecified;
        }
        set
        {
            this.parentArticleHierarchyIdFieldSpecified = value;
        }
    }

    /// <remarks/>
    public byte ArticleHierarchyLevelNo
    {
        get
        {
            return this.articleHierarchyLevelNoField;
        }
        set
        {
            this.articleHierarchyLevelNoField = value;
        }
    }

    /// <remarks/>
    public byte ArticleHierarchyStatusNo
    {
        get
        {
            return this.articleHierarchyStatusNoField;
        }
        set
        {
            this.articleHierarchyStatusNoField = value;
        }
    }

    /// <remarks/>
    public decimal DefaultMarkupPercentage
    {
        get
        {
            return this.defaultMarkupPercentageField;
        }
        set
        {
            this.defaultMarkupPercentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DefaultMarkupPercentageSpecified
    {
        get
        {
            return this.defaultMarkupPercentageFieldSpecified;
        }
        set
        {
            this.defaultMarkupPercentageFieldSpecified = value;
        }
    }

    /// <remarks/>
    public System.DateTime ModifiedDate
    {
        get
        {
            return this.modifiedDateField;
        }
        set
        {
            this.modifiedDateField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSArticleHierarchyDataArticleHierarchyExtraInfos
{

    private RSArticleHierarchyDataArticleHierarchyExtraInfosArticleHierarchyExtraInfo articleHierarchyExtraInfoField;

    /// <remarks/>
    public RSArticleHierarchyDataArticleHierarchyExtraInfosArticleHierarchyExtraInfo ArticleHierarchyExtraInfo
    {
        get
        {
            return this.articleHierarchyExtraInfoField;
        }
        set
        {
            this.articleHierarchyExtraInfoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RSArticleHierarchyDataArticleHierarchyExtraInfosArticleHierarchyExtraInfo
{

    private string extraInfoIdField;

    private string extraInfoValueField;

    private byte articleHierarchyIdField;

    /// <remarks/>
    public string ExtraInfoId
    {
        get
        {
            return this.extraInfoIdField;
        }
        set
        {
            this.extraInfoIdField = value;
        }
    }

    /// <remarks/>
    public string ExtraInfoValue
    {
        get
        {
            return this.extraInfoValueField;
        }
        set
        {
            this.extraInfoValueField = value;
        }
    }

    /// <remarks/>
    public byte ArticleHierarchyId
    {
        get
        {
            return this.articleHierarchyIdField;
        }
        set
        {
            this.articleHierarchyIdField = value;
        }
    }
}

