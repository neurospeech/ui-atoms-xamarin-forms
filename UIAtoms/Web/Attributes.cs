using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Web
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class RestAttribute : Attribute
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="RestAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public RestAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestAttribute"/> class.
        /// </summary>
        public RestAttribute()
        {

        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class VerbAttribute : RestAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="VerbAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="verb">The verb.</param>
        public VerbAttribute(string path, string verb) : base(path)
        {
            this.Verb = verb;
        }

        /// <summary>
        /// Gets or sets the verb.
        /// </summary>
        /// <value>
        /// The verb.
        /// </value>
        public string Verb { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class GetAttribute : VerbAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public GetAttribute(string path)
            : base(path, "Get")
        {

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PostAttribute : VerbAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PostAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public PostAttribute(string path) : base(path, "Post")
        {

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PutAttribute : VerbAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public PutAttribute(string path) : base(path, "Put")
        {

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class DeleteAttribute : VerbAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DeleteAttribute(string path) : base(path, "Delete")
        {

        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class ParamAttribute : RestAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamAttribute"/> class.
        /// </summary>
        public ParamAttribute()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ParamAttribute(string name) : base(name)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BodyAttribute : ParamAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyAttribute"/> class.
        /// </summary>
        public BodyAttribute()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueryAttribute : ParamAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public QueryAttribute(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAttribute"/> class.
        /// </summary>
        public QueryAttribute()
        {

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class PathAttribute : ParamAttribute
    {

        //public PathAttribute(string path):base(path)
        //{
        //}        
        /// <summary>
        /// Initializes a new instance of the <see cref="PathAttribute"/> class.
        /// </summary>
        public PathAttribute()
        {

        }



    }

    /// <summary>
    /// 
    /// </summary>
    public class FormAttribute : ParamAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FormAttribute(string path) : base(path)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAttribute"/> class.
        /// </summary>
        public FormAttribute()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RestServiceAttribute : Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RestServiceAttribute"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        public RestServiceAttribute(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestServiceAttribute"/> class.
        /// </summary>
        public RestServiceAttribute()
        {

        }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderAttribute : ParamAttribute
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public HeaderAttribute(string name) : base(name)
        {

        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class IgnoreHeaderAttribute : ParamAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreHeaderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public IgnoreHeaderAttribute(string name) : base(name)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ThrowsAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowsAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public ThrowsAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct RestParameter
    {
        /// <summary>
        /// The type
        /// </summary>
        public RestParameterType Type;

        /// <summary>
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// The value
        /// </summary>
        public object Value;
    }

    /// <summary>
    /// 
    /// </summary>
    public enum RestParameterType
    {
        /// <summary>
        /// The header
        /// </summary>
        Header,
        /// <summary>
        /// The query
        /// </summary>
        Query,
        /// <summary>
        /// The body
        /// </summary>
        Body,


        /// <summary>
        /// 
        /// </summary>
        BodyPath,
        /// <summary>
        /// The path
        /// </summary>
        Path,
        /// <summary>
        /// The form
        /// </summary>
        Form
    }
}
