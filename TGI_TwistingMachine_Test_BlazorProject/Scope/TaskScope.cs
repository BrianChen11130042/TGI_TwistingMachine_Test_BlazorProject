using CommonRazorLibraryB.Base.FiniteStateMachine;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Error;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Initial;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistMission;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistRegular;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.Main;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.SystemControl;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.SystemControl.Thread;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission.Thread;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistRegular;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistRegular.Thread;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public InitialTaskPack<ETwist> initialTaskPack;
        public ErrorTaskPack errorTaskPack;

        public InitialTask initialTask;
        public ErrorTask errorTask;

        void _initSysControlTask()
        {
            initialTaskPack = new InitialTaskPack<ETwist>(Enum.GetValues(typeof(ETwist)).Cast<ETwist>().ToList(),
                                                          twistLibrary,
                                                          twistLibrary,
                                                          initialDataLibrary);
            errorTaskPack = new ErrorTaskPack(errorDataLibrary);

            initialTask = new InitialTask(initialTaskPack);
            errorTask = new ErrorTask(errorTaskPack);

            initialTask.Set(ES1.None, EInitialTask.None, 0);
            errorTask.Set(ES1.None, EErrorTask.None, 0);
        }
    }

    public partial class TgiScope
    {
        public TwistRegularTaskPack<ETwist> twistRegularTaskPack;

        public TwistRegularTask twistRegularTask;

        void _initRegularTask()
        {
            twistRegularTaskPack = new TwistRegularTaskPack<ETwist>(Enum.GetValues(typeof(ETwist)).Cast<ETwist>().ToList(),
                                                                    twistLibrary,
                                                                    twistLibrary,
                                                                    allTwistRegularDataLibrary);

            twistRegularTask = new TwistRegularTask(twistRegularTaskPack);

            twistRegularTask.Set(ES1.None, ETwistRegular.None, 0);
        }
    }

    public partial class TgiScope
    {
        public TwistMissionTaskPack<ETwist> twistMissionTaskPack1;
        public TwistMissionTaskPack<ETwist> twistMissionTaskPack2;
        public TwistMissionTaskPack<ETwist> twistMissionTaskPack3;
        public TwistMissionTaskPack<ETwist> twistMissionTaskPack4;
        public TwistMissionTaskPack<ETwist> twistMissionTaskPack5;

        public TwistMissionTask twistMissionTask1;
        public TwistMissionTask twistMissionTask2;
        public TwistMissionTask twistMissionTask3;
        public TwistMissionTask twistMissionTask4;
        public TwistMissionTask twistMissionTask5;

        void _initMissionTask()
        {
            twistMissionTaskPack1 = new TwistMissionTaskPack<ETwist>(ETwist.Twist1, twistLibrary, 
                                                                     twistLibrary, twist1MissionDataLibrary);
            twistMissionTaskPack2 = new TwistMissionTaskPack<ETwist>(ETwist.Twist2, twistLibrary,
                                                                     twistLibrary, twist2MissionDataLibrary);
            twistMissionTaskPack3 = new TwistMissionTaskPack<ETwist>(ETwist.Twist3, twistLibrary,
                                                                     twistLibrary, twist3MissionDataLibrary);
            twistMissionTaskPack4 = new TwistMissionTaskPack<ETwist>(ETwist.Twist4, twistLibrary,
                                                                     twistLibrary, twist4MissionDataLibrary);
            twistMissionTaskPack5 = new TwistMissionTaskPack<ETwist>(ETwist.Twist5, twistLibrary,
                                                                     twistLibrary, twist5MissionDataLibrary);

            twistMissionTask1 = new TwistMissionTask(twistMissionTaskPack1);
            twistMissionTask2 = new TwistMissionTask(twistMissionTaskPack2);
            twistMissionTask3 = new TwistMissionTask(twistMissionTaskPack3);
            twistMissionTask4 = new TwistMissionTask(twistMissionTaskPack4);
            twistMissionTask5 = new TwistMissionTask(twistMissionTaskPack5);

            twistMissionTask1.Set(ES1.None, ETwistMission.None, 0);
            twistMissionTask2.Set(ES1.None, ETwistMission.None, 0);
            twistMissionTask3.Set(ES1.None, ETwistMission.None, 0);
            twistMissionTask4.Set(ES1.None, ETwistMission.None, 0);
            twistMissionTask5.Set(ES1.None, ETwistMission.None, 0);
        }
    }

    public partial class TgiScope
    {
        public SystemControlThread systemControlThread;
        public TwistRegularThread twistRegularThread;
        public TwistMissionThread twistMissionThread;

        public MainThread mainThread;

        void _initThread()
        {
            systemControlThread = new SystemControlThread(initialTask, errorTask);
            systemControlThread.Set(ES1.None, ESystemControlThread.None, 0);

            twistRegularThread = new TwistRegularThread(twistRegularTask);
            twistRegularThread.Set(ES1.None, ETwistRegularThread.None, 0);

            twistMissionThread = new TwistMissionThread(twistMissionTask1,
                                                        twistMissionTask2,
                                                        twistMissionTask3,
                                                        twistMissionTask4,
                                                        twistMissionTask5);
            twistMissionThread.Set(ES1.None, ETwistMissionThread.None, 0);

            mainThread = new MainThread(systemControlThread, twistRegularThread, twistMissionThread);
            mainThread.Set(ES1.Init, EMainThread.None, 0);
        }

        private CancellationTokenSource _ctsMain;
        private CancellationTokenSource _ctsSystemControl;
        private CancellationTokenSource _ctsTwistRegular;
        private CancellationTokenSource _ctsTwistMission;

        private Task _mainTask;
        private Task _systemControlTask;
        private Task _twistRegularTask;
        private Task _twistMissionTask;

        public void _startThread()
        {
            if (_mainTask == null || _mainTask.IsCompleted)
            {
                _ctsMain = new CancellationTokenSource();
                _mainTask = StartLongRunning(async () => await RunMainAsync(_ctsMain.Token));
            }

            if (_systemControlTask == null || _systemControlTask.IsCompleted)
            {
                _ctsSystemControl = new CancellationTokenSource();
                _systemControlTask = StartLongRunning(async () => await RunSystemControlAsync(_ctsSystemControl.Token));
            }

            if (_twistRegularTask == null || _twistRegularTask.IsCompleted)
            {
                _ctsTwistRegular = new CancellationTokenSource();
                _twistRegularTask = StartLongRunning(async () => await RunTwistRegularAsync(_ctsTwistRegular.Token));
            }

            if (_twistMissionTask == null || _twistMissionTask.IsCompleted)
            {
                _ctsTwistMission = new CancellationTokenSource();
                _twistMissionTask = StartLongRunning(async () => await RunTwistMissionAsync(_ctsTwistMission.Token));
            }
        }

        public void _stopThread()
        {
            _ctsMain?.Cancel();
            _ctsSystemControl?.Cancel();
            _ctsTwistRegular?.Cancel();
            _ctsTwistMission?.Cancel();

            // 強制等待所有 Task 結束，最多等 60 秒，避免死結
            Task.WaitAll(new[] { _mainTask, _systemControlTask, _twistRegularTask, _twistMissionTask }
                        .Where(t => t != null)
                        .ToArray(), TimeSpan.FromSeconds(60));
        }

        private Task StartLongRunning(Func<Task> func)
        {
            // 開啟「專屬 thread」而不是 ThreadPool
            return Task.Factory.StartNew(
                async () => await func(),
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
            ).Unwrap();
        }

        private async Task RunMainAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await mainThread.Run();

                    await Task.Delay(300, token);
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"Main 例外: {ex}");
                }
            }
        }

        private async Task RunSystemControlAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await systemControlThread.Run();

                    await Task.Delay(100, token);
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"System Control 例外: {ex}");
                }
            }
        }

        private async Task RunTwistRegularAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await twistRegularThread.Run();

                    await Task.Delay(500, token);
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"Twist Regular 例外: {ex}");
                }
            }
        }

        private async Task RunTwistMissionAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await twistMissionThread.Run();

                    await Task.Delay(100, token);
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"Twist Mission 例外: {ex}");
                }
            }
        }
    }
}
