﻿namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Extensions.Primitives;
    using Http;
    using Net.Http.Headers;

    /// <summary>
    /// A <see cref="StatusCodeResult"/> that when executed will produce an empty
    /// <see cref="StatusCodes.Status416RequestedRangeNotSatisfiable"/> response.
    /// </summary>
    public class RequestedRangeNotSatisfiableResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedRangeNotSatisfiableResult"/> class.
        /// </summary>
        public RequestedRangeNotSatisfiableResult()
            : base(StatusCodes.Status416RequestedRangeNotSatisfiable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedRangeNotSatisfiableResult"/> class.
        /// </summary>
        /// <param name="selectedResourceLength"></param>
        public RequestedRangeNotSatisfiableResult(long? selectedResourceLength)
            : this()
        {
            this.SelectedResourceLength = selectedResourceLength;
        }

        /// <summary>
        /// Gets or sets the current length of the selected resource.
        /// </summary>
        public long? SelectedResourceLength { get; set; }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (SelectedResourceLength.HasValue)
            {
                context.HttpContext.Response.Headers.Add(HeaderNames.ContentRange, new StringValues(this.SelectedResourceLength.ToString()));
            }

            return base.ExecuteResultAsync(context);
        }
    }
}
