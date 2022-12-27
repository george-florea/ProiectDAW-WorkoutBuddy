using Backend.Common;
using Backend.Entities;
using Backend.Entities;

namespace Backend.DataAccess
{
    public class UnitOfWork
    {
        private readonly WorkoutBuddyDBContext Context;

        public UnitOfWork(WorkoutBuddyDBContext context)
        {
            Context = context;
        }

        private IRepository<Comment> comments;
        public IRepository<Comment> Comments => comments ?? (comments = new BaseRepository<Comment>(Context));

        private IRepository<Exercise> exercises;
        public IRepository<Exercise> Exercises => exercises ?? (exercises = new BaseRepository<Exercise>(Context));

        private IRepository<ExerciseType> exerciseTypes;
        public IRepository<ExerciseType> ExerciseTypes => exerciseTypes ?? (exerciseTypes = new BaseRepository<ExerciseType>(Context));

        private IRepository<Image> images;
        public IRepository<Image> Images => images ?? (images = new BaseRepository<Image>(Context));

        private IRepository<MuscleGroup> muscleGroups;
        public IRepository<MuscleGroup> MuscleGroups => muscleGroups ?? (muscleGroups = new BaseRepository<MuscleGroup>(Context));

        private IRepository<Permission> permissions;
        public IRepository<Permission> Permissions => permissions ?? (permissions = new BaseRepository<Permission>(Context));

        private IRepository<Reason> reasons;
        public IRepository<Reason> Reasons => reasons ?? (reasons = new BaseRepository<Reason>(Context));

        private IRepository<Role> roles;
        public IRepository<Role> Roles => roles ?? (roles = new BaseRepository<Role>(Context));

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<Split> splits;
        public IRepository<Split> Splits => splits ?? (splits = new BaseRepository<Split>(Context));

        private IRepository<UserExercise> userExercises;
        public IRepository<UserExercise> UserExercises => userExercises ?? (userExercises = new BaseRepository<UserExercise>(Context));

        private IRepository<UserExercisePr> userExerciseprs;
        public IRepository<UserExercisePr> UserExerciseprs => userExerciseprs ?? (userExerciseprs = new BaseRepository<UserExercisePr>(Context));

        private IRepository<UserExerciseSet> userExerciseSets;
        public IRepository<UserExerciseSet> UserExerciseSets => userExerciseSets ?? (userExerciseSets = new BaseRepository<UserExerciseSet>(Context));

        private IRepository<UserPointsHistory> userPointsHistorys;
        public IRepository<UserPointsHistory> UserPointsHistorys => userPointsHistorys ?? (userPointsHistorys = new BaseRepository<UserPointsHistory>(Context));

        private IRepository<UserSplit> userSplits;
        public IRepository<UserSplit> UserSplits => userSplits ?? (userSplits = new BaseRepository<UserSplit>(Context));

        private IRepository<UserWeightHistory> userWeightHistorys;
        public IRepository<UserWeightHistory> UserWeightHistorys => userWeightHistorys ?? (userWeightHistorys = new BaseRepository<UserWeightHistory>(Context));

        private IRepository<UserWorkout> userWorkouts;
        public IRepository<UserWorkout> UserWorkouts => userWorkouts ?? (userWorkouts = new BaseRepository<UserWorkout>(Context));

        private IRepository<Workout> workouts;
        public IRepository<Workout> Workouts => workouts ?? (workouts = new BaseRepository<Workout>(Context));

        private IRepository<WorkoutExercise> workoutExercises;
        public IRepository<WorkoutExercise> WorkoutExercises => workoutExercises ?? (workoutExercises = new BaseRepository<WorkoutExercise>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
