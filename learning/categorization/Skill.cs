using core;
using Microsoft.ML;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace categorization
{
    /// <summary>
    /// Predicts the user's intent from a text sample
    /// </summary>
    public class Skill : IPredictor
    {
        public async Task<Prediction> Predict(ISettings settings, string text)
        {
            var context = new MLContext(seed: 0);
            var modelPath = settings.ModelPath;

            ITransformer model;
            if (File.Exists(modelPath))
            {
                model = context.Model.Load(modelPath, out _);
            }
            else
            {
                var data = Training();
                var trainingDataView = context.Data.LoadFromEnumerable(data);
                var pipeline = await BuildPipeline(context);
                var trainingPipeline = await BuildTrainingPipeline(context, pipeline);

                model = await BuildModel(trainingDataView, trainingPipeline);
                var folder = Path.GetDirectoryName(modelPath);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                File.WriteAllBytes(modelPath, new byte[] { 0 });
                context.Model.Save(model, trainingDataView.Schema, modelPath);
            }

            var engine = context.Model
                .CreatePredictionEngine<Token, Prediction>(model);

            var token = new Token()
            {
                Description = text
            };

            var prediction = engine.Predict(token);
            return new Prediction(prediction.Name);
        }

        private static IEnumerable<Token> Entries()
        {
            var ids = 0;
            return new List<Token>
            {
                new Token(ids++, "Entry", "add entries"),
                new Token(ids++, "Entry", "add entry"),
                new Token(ids++, "Entry", "enter journaling"),
                new Token(ids++, "Entry", "add journaling"),
                new Token(ids++, "Entry", "add journal"),
                new Token(ids++, "Entry", "add diary"),
                new Token(ids++, "Entry", "add chronicle"),
                new Token(ids++, "Entry", "add daybook"),
                new Token(ids++, "Entry", "add log"),
                new Token(ids++, "Entry", "add annals"),
                new Token(ids++, "Entry", "add blog"),
                new Token(ids++, "Entry", "add history"),
                new Token(ids++, "Entry", "add notebook"),
                new Token(ids++, "Entry", "add yearbook"),
                new Token(ids++, "Entry", "add logbook"),
                new Token(ids++, "Entry", "add moblog"),
                new Token(ids++, "Entry", "add record"),
                new Token(ids++, "Entry", "add register"),
                new Token(ids++, "Entry", "add vlog"),
                new Token(ids++, "Entry", "add weblog"),
                new Token(ids++, "Entry", "add day-by-day"),
                new Token(ids++, "Entry", "add record"),
                new Token(ids++, "Entry", "add ledger"),
                new Token(ids++, "Entry", "add memorandum"),
                new Token(ids++, "Entry", "add notepad"),

                new Token(ids++, "Entry", "create entries"),
                new Token(ids++, "Entry", "create entry"),
                new Token(ids++, "Entry", "enter journaling"),
                new Token(ids++, "Entry", "create journaling"),
                new Token(ids++, "Entry", "create journal"),
                new Token(ids++, "Entry", "create diary"),
                new Token(ids++, "Entry", "create chronicle"),
                new Token(ids++, "Entry", "create daybook"),
                new Token(ids++, "Entry", "create log"),
                new Token(ids++, "Entry", "create annals"),
                new Token(ids++, "Entry", "create blog"),
                new Token(ids++, "Entry", "create history"),
                new Token(ids++, "Entry", "create notebook"),
                new Token(ids++, "Entry", "create yearbook"),
                new Token(ids++, "Entry", "create logbook"),
                new Token(ids++, "Entry", "create moblog"),
                new Token(ids++, "Entry", "create record"),
                new Token(ids++, "Entry", "create register"),
                new Token(ids++, "Entry", "create vlog"),
                new Token(ids++, "Entry", "create weblog"),
                new Token(ids++, "Entry", "create day-by-day"),
                new Token(ids++, "Entry", "create record"),
                new Token(ids++, "Entry", "create ledger"),
                new Token(ids++, "Entry", "create memorandum"),
                new Token(ids++, "Entry", "create notepad"),

                new Token(ids++, "Entry", "enter entries"),
                new Token(ids++, "Entry", "enter entry"),
                new Token(ids++, "Entry", "enter journaling"),
                new Token(ids++, "Entry", "enter journaling"),
                new Token(ids++, "Entry", "enter journal"),
                new Token(ids++, "Entry", "enter diary"),
                new Token(ids++, "Entry", "enter chronicle"),
                new Token(ids++, "Entry", "enter daybook"),
                new Token(ids++, "Entry", "enter log"),
                new Token(ids++, "Entry", "enter annals"),
                new Token(ids++, "Entry", "enter blog"),
                new Token(ids++, "Entry", "enter history"),
                new Token(ids++, "Entry", "enter notebook"),
                new Token(ids++, "Entry", "enter yearbook"),
                new Token(ids++, "Entry", "enter logbook"),
                new Token(ids++, "Entry", "enter moblog"),
                new Token(ids++, "Entry", "enter record"),
                new Token(ids++, "Entry", "enter register"),
                new Token(ids++, "Entry", "enter vlog"),
                new Token(ids++, "Entry", "enter weblog"),
                new Token(ids++, "Entry", "enter day-by-day"),
                new Token(ids++, "Entry", "enter record"),
                new Token(ids++, "Entry", "enter ledger"),
                new Token(ids++, "Entry", "enter memorandum"),
                new Token(ids, "Entry", "enter notepad"),
            };
        }

        private static IEnumerable<Token> Expenses()
        {
            var ids = 0;
            return new List<Token>
            {
                new Token(ids++, "Expense", "expenses"),
                new Token(ids++, "Expense", "add expenses"),
                new Token(ids++, "Expense", "add expense"),
                new Token(ids++, "Expense", "add purchases"),
                new Token(ids++, "Expense", "add purchase"),
                new Token(ids++, "Expense", "add charge"),
                new Token(ids++, "Expense", "add cost"),
                new Token(ids++, "Expense", "add outlay"),
                new Token(ids++, "Expense", "add payment"),
                new Token(ids++, "Expense", "add toll"),
                new Token(ids++, "Expense", "add fee"),
                new Token(ids++, "Expense", "add price"),
                new Token(ids++, "Expense", "add tariff"),
                new Token(ids++, "Expense", "add expenditure"),
                new Token(ids++, "Expense", "add levy"),
                new Token(ids++, "Expense", "add spending"),
                new Token(ids++, "Expense", "add consumption"),
                new Token(ids++, "Expense", "add damage"),
                new Token(ids++, "Expense", "add disbursement"),
                new Token(ids++, "Expense", "add assessment"),
                new Token(ids++, "Expense", "add debit"),
                new Token(ids++, "Expense", "add liability"),
                new Token(ids++, "Expense", "add surcharge"),

                new Token(ids++, "Expense", "enter expenses"),
                new Token(ids++, "Expense", "enter expense"),
                new Token(ids++, "Expense", "enter purchases"),
                new Token(ids++, "Expense", "enter purchase"),
                new Token(ids++, "Expense", "enter charge"),
                new Token(ids++, "Expense", "enter cost"),
                new Token(ids++, "Expense", "enter outlay"),
                new Token(ids++, "Expense", "enter payment"),
                new Token(ids++, "Expense", "enter toll"),
                new Token(ids++, "Expense", "enter fee"),
                new Token(ids++, "Expense", "enter price"),
                new Token(ids++, "Expense", "enter tariff"),
                new Token(ids++, "Expense", "enter expenditure"),
                new Token(ids++, "Expense", "enter levy"),
                new Token(ids++, "Expense", "enter spending"),
                new Token(ids++, "Expense", "enter consumption"),
                new Token(ids++, "Expense", "enter damage"),
                new Token(ids++, "Expense", "enter disbursement"),
                new Token(ids++, "Expense", "enter assessment"),
                new Token(ids++, "Expense", "enter debit"),
                new Token(ids++, "Expense", "enter liability"),
                new Token(ids++, "Expense", "enter surcharge"),

                new Token(ids++, "Expense", "record expenses"),
                new Token(ids++, "Expense", "record expense"),
                new Token(ids++, "Expense", "record purchases"),
                new Token(ids++, "Expense", "record purchase"),

                new Token(ids++, "Expense", "create expenses"),
                new Token(ids++, "Expense", "create expense"),
                new Token(ids++, "Expense", "create purchases"),
                new Token(ids++, "Expense", "create purchase"),
                new Token(ids++, "Expense", "create charge"),
                new Token(ids++, "Expense", "create cost"),
                new Token(ids++, "Expense", "create outlay"),
                new Token(ids++, "Expense", "create payment"),
                new Token(ids++, "Expense", "create toll"),
                new Token(ids++, "Expense", "create fee"),
                new Token(ids++, "Expense", "create price"),
                new Token(ids++, "Expense", "create tariff"),
                new Token(ids++, "Expense", "create expenditure"),
                new Token(ids++, "Expense", "create levy"),
                new Token(ids++, "Expense", "create spending"),
                new Token(ids++, "Expense", "create consumption"),
                new Token(ids++, "Expense", "create damage"),
                new Token(ids++, "Expense", "create disbursement"),
                new Token(ids++, "Expense", "create assessment"),
                new Token(ids++, "Expense", "create debit"),
                new Token(ids++, "Expense", "create liability"),
                new Token(ids, "Expense", "create surcharge"),
            };
        }

        private static IEnumerable<Token> Training()
        {
            var results = new List<Token>();
            results.AddRange(Expenses());
            results.AddRange(Entries());
            return results;
        }

        private Task<ITransformer> BuildModel(IDataView dataView, IEstimator<ITransformer> trainingPipeline)
        {
            return Task.Run(() => trainingPipeline.Fit(dataView));
        }

        private Task<IEstimator<ITransformer>> BuildTrainingPipeline(MLContext context, IEstimator<ITransformer> pipeline)
        {
            return Task.Run(() =>
            {
                var trainingPipeline = pipeline.Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                    .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

                return (IEstimator<ITransformer>)trainingPipeline;
            });
        }

        private Task<IEstimator<ITransformer>> BuildPipeline(MLContext context)
        {
            return Task.Run(() =>
            {
                var pipeline = context.Transforms.Conversion
                    .MapValueToKey(inputColumnName: "Name", outputColumnName: "Label")
                    .Append(context.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                    .Append(context.Transforms.Concatenate("Features", "DescriptionFeaturized"))
                    .AppendCacheCheckpoint(context);

                return (IEstimator<ITransformer>)pipeline;
            });
        }
    }
}