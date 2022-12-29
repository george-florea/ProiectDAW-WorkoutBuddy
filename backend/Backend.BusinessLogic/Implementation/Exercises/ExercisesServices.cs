using Backend.BusinessLogic.Base;
using Backend.Entities;
using Backend.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Exercises
{
    public class ExerciseService : BaseService
    {
        //private readonly AddExerciseValidator AddExerciseValidator;
        //private readonly EditExerciseValidator EditExerciseValidator;
        public ExerciseService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            //AddExerciseValidator = new AddExerciseValidator(UnitOfWork);
            //EditExerciseValidator = new EditExerciseValidator(UnitOfWork);
        }

        /*public int NumberOfPages(int pageSize)
        {
            var exercisesNo = UnitOfWork.Exercises.Get().ToList().Count;
            return exercisesNo / pageSize + 1;
        }*/

        public async Task<List<ExerciseAsListItemModel>> GetExercises()
        {
            var exercisesList = new List<ExerciseAsListItemModel>();
            var listOfExercises = await UnitOfWork.Exercises
                .Get()
                .Where(e => e.IsPending != true)
                .OrderBy(e => e.Name)
                /*.Skip(pageSize * index)
                .Take(pageSize)*/
                .ToListAsync();

            if (listOfExercises == null)
            {
                return exercisesList;
            }

            foreach (var exercise in listOfExercises)
            {
                var exerciseModel = new ExerciseAsListItemModel()
                {
                    ExerciseId = exercise.Idexercise,
                    Name = exercise.Name,
                    IdImage = exercise.Idimage,
                    ExerciseType = Enum.GetName(typeof(ExerciseTypes), exercise.Idtype)
                };

                exercisesList.Add(exerciseModel);
            }
            return exercisesList;
        }
/*
        public List<ExerciseAsListItemModel> GetPendingExercises()
        {
            var exercisesList = new List<ExerciseAsListItemModel>();
            var listOfExercises = UnitOfWork.Exercises
                .Get()
                .Where(e => e.IsPending == true)
                .OrderBy(e => e.Name)
                .ToList();

            if (listOfExercises == null)
            {
                return exercisesList;
            }

            foreach (var exercise in listOfExercises)
            {
                var exerciseType = UnitOfWork.ExerciseTypes.Get().FirstOrDefault(t => t.Idtype == exercise.Idtype).Type;
                var exerciseModel = Mapper.Map<Exercise, ExerciseAsListItemModel>(exercise);
                exerciseModel.ExerciseType = exerciseType;
                exercisesList.Add(exerciseModel);
            }
            return exercisesList;
        }

        public bool ApproveExercise(Guid id)
        {
            var isOk = true;
            ExecuteInTransaction(uow =>
            {
                var exercise = uow.Exercises.Get()
                                .FirstOrDefault(e => e.Idexercise == id);
                if (exercise == null)
                {
                    throw new NotFoundErrorException("this exercise does not exist");
                }

                exercise.IsPending = false;

                try
                {
                    uow.Exercises.Update(exercise);
                    uow.SaveChanges();

                }
                catch (Exception e)
                {
                    isOk = false;
                }

            });
            return isOk;
        }

        public ExercisesModel GetExercise(Guid exerciseId)
        {
            var exercise = UnitOfWork.Exercises.Get()
                .Include(e => e.Idgroups)
                .FirstOrDefault(e => e.Idexercise == exerciseId);
            if (exercise == null)
            {
                throw new NotFoundErrorException("Exercise not found");
            }

            var mappings = UnitOfWork.ExerciseTypes.Get().FirstOrDefault(t => t.Idtype == exercise.Idtype);
            var model = Mapper.Map<Exercise, ExercisesModel>(exercise);
            model.ExerciseType = mappings.Type;
            return model;
        }

        public void AddExercise(AddExerciseModel model)
        {
            ExecuteInTransaction(uow =>
            {

                var validationRes = AddExerciseValidator.Validate(model);

                if (!validationRes.IsValid)
                {
                    var exerciseTypes = Enum.GetValues(typeof(ExerciseTypes)).Cast<ExerciseTypes>()
                           .Select(v => new ListItemModel<string, int>
                           {
                               Text = v.ToString(),
                               Value = ((int)v)
                           }).ToList();
                    var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                        .Select(v => new System.Web.Mvc.SelectListItem()
                        {
                            Text = v.ToString(),
                            Value = ((int)v).ToString(),
                        }).ToList();
                    model.ExerciseTypes = exerciseTypes;
                    model.MuscleGroups = muscleGroups;

                    validationRes.ThenThrow(model);
                }


                var image = new Image();
                image.Idimg = Guid.NewGuid();
                using (var ms = new MemoryStream())
                {
                    model.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    image.ImgContent = fileBytes;
                }

                var typeEntity = uow.ExerciseTypes.Get().FirstOrDefault(t => t.Idtype == model.SelectedType);

                var exercise = Mapper.Map<AddExerciseModel, Exercise>(model);
                exercise.Idimage = image.Idimg;
                exercise.IdimageNavigation = image;
                exercise.IdtypeNavigation = typeEntity;

                var musclesList = uow.MuscleGroups
                                .Get()
                                .Where(g => model.SelectedMuscleGroups.Contains(g.Idgroup))
                                .ToList();

                musclesList.ForEach(m => exercise.Idgroups.Add(m));
                exercise.Idgroups = musclesList;

                uow.Exercises.Insert(exercise);
                uow.SaveChanges();
            });
        }

        public void DeleteExercise(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var exercise = uow.Exercises
                            .Get()
                            .Include(e => e.Idgroups)
                            .Include(e => e.IdimageNavigation)
                            .Include(e => e.IdtypeNavigation)
                            .FirstOrDefault(e => e.Idexercise == id);
                if (exercise == null)
                {
                    throw new NotFoundErrorException("Exercise not found");
                }
                exercise.Idgroups.Clear();
                uow.Exercises.Delete(exercise);
                uow.SaveChanges();
            });
        }

        public EditExerciseModel PopulateEditModel(Guid id)
        {
            var exerciseTypes = Enum.GetValues(typeof(ExerciseTypes)).Cast<ExerciseTypes>()
                           .Select(v => new ListItemModel<string, int>
                           {
                               Text = v.ToString(),
                               Value = ((int)v)
                           }).ToList();
            var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                .Select(v => new System.Web.Mvc.SelectListItem()
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                }).ToList();

            var exercise = UnitOfWork.Exercises.Get()
                            .Include(e => e.Idgroups)
                            .Include(e => e.IdimageNavigation)
                            .Include(e => e.IdtypeNavigation)
                            .FirstOrDefault(e => e.Idexercise == id);

            if (exercise == null)
            {
                throw new NotFoundErrorException("Exercise not found");
            }

            var selectedGroups = exercise.Idgroups.Select(g => g.Idgroup).ToList();

            var model = Mapper.Map<Exercise, EditExerciseModel>(exercise);
            model.ExerciseTypes = exerciseTypes;
            model.SelectedMuscleGroups = selectedGroups;
            model.MuscleGroups = muscleGroups;
            return model;
        }

        public void EditExercise(EditExerciseModel model)
        {
            ExecuteInTransaction(uow =>
            {
                var validationRes = EditExerciseValidator.Validate(model);

                if (!validationRes.IsValid)
                {
                    var returnModel = PopulateEditModel(model.ExerciseId);
                    validationRes.ThenThrow(returnModel);
                }

                var exercise = uow.Exercises.Get()
                            .Include(e => e.Idgroups)
                            .Include(e => e.IdimageNavigation)
                            .Include(e => e.IdtypeNavigation)
                            .FirstOrDefault(e => e.Idexercise == model.ExerciseId);

                if (exercise == null)
                {
                    throw new NotFoundErrorException("Exercise not found");
                }

                if (model.Image != null)
                {
                    var image = exercise.IdimageNavigation;
                    if (image == null)
                    {
                        image = new Image();
                        image.Idimg = Guid.NewGuid();
                    }

                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        image.ImgContent = fileBytes;
                    }
                    exercise.Idimage = image.Idimg;
                    exercise.IdimageNavigation = image;
                }

                var typeEntity = uow.ExerciseTypes.Get().FirstOrDefault(t => t.Idtype == model.SelectedType);
                exercise.Idtype = typeEntity.Idtype;
                exercise.IdtypeNavigation = typeEntity;
                exercise.Name = model.Name;
                exercise.Description = model.Description;

                exercise.Idgroups.Clear();
                var musclesList = uow.MuscleGroups
                    .Get()
                    .Where(g => model.SelectedMuscleGroups.Contains(g.Idgroup))
                    .ToList();

                musclesList.ForEach(m => exercise.Idgroups.Add(m));

                uow.Exercises.Update(exercise);
                uow.SaveChanges();
            });
        }

        public List<SplitExerciseModel> GetFilteredExercises(List<string> selectedGroups)
        {
            var groups = UnitOfWork.MuscleGroups.Get()
                .Where(m => selectedGroups.Contains(m.Idgroup.ToString()));

            if (groups == null)
            {
                throw new NotFoundErrorException("invalid muscle groups!");
            }
            var exercises = groups.SelectMany(m => m.Idexercises)
                            .Where(e => e.IsPending != true)
                            .Distinct()
                            .ToList();

            var list = new List<SplitExerciseModel>();

            foreach (var ex in exercises)
            {
                list.Add(new SplitExerciseModel()
                {
                    ExerciseId = ex.Idexercise,
                    ExerciseName = ex.Name
                });
            }

            return list;
        }*/
    }
}