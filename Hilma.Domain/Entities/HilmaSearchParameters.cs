using Newtonsoft.Json;

namespace Hilma.Domain.Entities {
    /// <summary>
    ///     Search parameters for azure search
    /// </summary>
    public class HilmaSearchParameters
    {
        /// <summary>
        /// Gets or sets a value that specifies whether to fetch the total
        /// count of results. Default is false. Setting this value to true may
        /// have a performance impact. Note that the count returned is an
        /// approximation.
        /// </summary>
        public bool IncludeTotalResultCount { get; set; }

        /// <summary>
        /// Gets or sets the list of facet expressions to apply to the search
        /// query. Each facet expression contains a field name, optionally
        /// followed by a comma-separated list of name:value pairs.
        /// </summary>
        public string[] Facets { get; set; }

        /// <summary>
        /// Gets or sets the OData $filter expression to apply to the search
        /// query.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the list of field names to use for hit highlights.
        /// Only searchable fields can be used for hit highlighting.
        /// </summary>
        public string[] HighlightFields { get; set; }

        /// <summary>
        /// Gets or sets a string tag that is appended to hit highlights. Must
        /// be set with highlightPreTag. Default is
        /// &amp;amp;lt;/em&amp;amp;gt;.
        /// </summary>
        public string HighlightPostTag { get; set; }

        /// <summary>
        /// Gets or sets a string tag that is prepended to hit highlights. Must
        /// be set with highlightPostTag. Default is
        /// &amp;amp;lt;em&amp;amp;gt;.
        /// </summary>
        public string HighlightPreTag { get; set; }

        /// <summary>
        /// Gets or sets a number between 0 and 100 indicating the percentage
        /// of the index that must be covered by a search query in order for
        /// the query to be reported as a success. This parameter can be useful
        /// for ensuring search availability even for services with only one
        /// replica. The default is 100.
        /// </summary>
        public double? MinimumCoverage { get; set; }

        /// <summary>
        /// Gets or sets the list of OData $orderby expressions by which to
        /// sort the results. Each expression can be either a field name or a
        /// call to either the geo.distance() or the search.score() functions.
        /// Each expression can be followed by asc to indicate ascending, and
        /// desc to indicate descending. The default is ascending order. Ties
        /// will be broken by the match scores of documents. If no OrderBy is
        /// specified, the default sort order is descending by document match
        /// score. There can be at most 32 $orderby clauses.
        /// </summary>
        public string[] OrderBy { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the syntax of the search query.
        /// The default is 'simple'. Use 'full' if your query uses the Lucene
        /// query syntax. Possible values include: 'simple', 'full'
        /// </summary>
        //public QueryType QueryType { get; set; }

        /// <summary>
        /// Gets or sets the list of parameter values to be used in scoring
        /// functions (for example, referencePointParameter) using the format
        /// name-values. For example, if the scoring profile defines a function
        /// with a parameter called 'mylocation' the parameter string would be
        /// "mylocation--122.2,44.8" (without the quotes).
        /// </summary>
        //public ScoringParameter[] ScoringParameters { get; set; }
    
        /// <summary>
        /// Gets or sets the name of a scoring profile to evaluate match scores
        /// for matching documents in order to sort the results.
        /// </summary>
        public string ScoringProfile { get; set; }

        /// <summary>
        /// Gets or sets the list of field names to which to scope the
        /// full-text search. When using fielded search
        /// (fieldName:searchExpression) in a full Lucene query, the field
        /// names of each fielded search expression take precedence over any
        /// field names listed in this parameter.
        /// </summary>
        public string[] SearchFields { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies whether any or all of the
        /// search terms must be matched in order to count the document as a
        /// match. Possible values include: 'any', 'all'
        /// </summary>
        //public SearchMode SearchMode { get; set; }

        /// <summary>
        /// Gets or sets the list of fields to retrieve. If unspecified, all
        /// fields marked as retrievable in the schema are included.
        /// </summary>
        public string[] Select { get; set; }

        /// <summary>
        /// Gets or sets the number of search results to skip. This value
        /// cannot be greater than 100,000. If you need to scan documents in
        /// sequence, but cannot use $skip due to this limitation, consider
        /// using $orderby on a totally-ordered key and $filter with a range
        /// query instead.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of search results to retrieve. This can be
        /// used in conjunction with $skip to implement client-side paging of
        /// search results. If results are truncated due to server-side paging,
        /// the response will include a continuation token that can be used to
        /// issue another Search request for the next page of results.
        /// </summary>
        public int? Top { get; set; }
        /// <summary>
        ///     Search query for azure search.
        /// </summary>
        public string Search { get; set; }
    }
}
