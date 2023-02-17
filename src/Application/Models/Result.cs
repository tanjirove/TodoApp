using System.Collections.Generic;
using System.Linq;

namespace Todo.Application.Models
{
    public class Result
    {
        internal Result()
        {
            Errors = new List<string>();
        }

        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public static Result Succeed()
        {
            return new Result { Success = true };
        }
    }

    public class CommandResult
    {
        internal CommandResult()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public bool Success { get; set; } = false;
        public string SuccessMessage { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public static CommandResult Succeed()
        {
            return new CommandResult { Success = true };
        }

        public static CommandResult Succeed(string successMessage)
        {
            return new CommandResult { Success = true, SuccessMessage = successMessage };
        }

        public static CommandResult Failed(IDictionary<string, string[]> errors)
        {
            return new CommandResult { Success = false, Errors = errors };
        }

        public static CommandResult Failed(string errorMessage)
        {
            var errors = new Dictionary<string, string[]>
            {
                {"", new[] { errorMessage } }
            };

            return Failed(errors);
        }
    }

    public class QueryResult
    {
        internal QueryResult()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public bool Success { get; set; } = true;
        public IDictionary<string, string[]> Errors { get; set; }

        public static QueryResult Succeed()
        {
            return new QueryResult { Success = true };
        }

        public static QueryResult Failed(IDictionary<string, string[]> errors)
        {
            return new QueryResult { Success = false, Errors = errors };
        }

        public static QueryResult Failed(string errorMessage)
        {
            var errors = new Dictionary<string, string[]>
            {
                {"", new[] { errorMessage } }
            };

            return Failed(errors);
        }

        public static QueryResult Failed(IEnumerable<string> errorMessages)
        {
            var errors = errorMessages
                .ToDictionary(x => "", x => new[] { x });

            return Failed(errors);
        }
    }
}
