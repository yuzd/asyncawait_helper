
using System.Runtime.CompilerServices;

// static async Task Main(string[] args)
// {
// 	async Task test1(){
// 		Console.WriteLine("test1-start");
// 		await test2();
// 		Console.WriteLine("test1-end");
//
// 	}
//
// 	async Task test2()
// 	{
// 		Console.WriteLine("test2-start");
// 		await Task.Delay(1000);
// 		Console.WriteLine("test2-end");
// 	}
//
// 	await test1();
//             
// 	Console.WriteLine("Let's Go!");
// }

// 生成的代碼示意如下：

StateMachineFactory.CreateMainAsyncStateMachine().GetAwaiter().GetResult();

class StateMachineFactory
{
	internal static Task CreateMainAsyncStateMachine()
	{
		MainAsyncStateMachine stateMachine = new MainAsyncStateMachine
		{
			_builder = AsyncTaskMethodBuilder.Create(),
			_state = -1
		};
		stateMachine._builder.Start(ref stateMachine);
		return stateMachine._builder.Task;
	}
	internal static Task CreateTest1AsyncStateMachine()
	{
		Test1AsyncStateMachine stateMachine = new Test1AsyncStateMachine
		{
			_builder = AsyncTaskMethodBuilder.Create(),
			_state = -1
		};
		stateMachine._builder.Start(ref stateMachine);
		return stateMachine._builder.Task;
	}

	internal static Task CreateTest2AsyncStateMachine()
	{
		Test2AsyncStateMachine stateMachine = new Test2AsyncStateMachine
		{
			_builder = AsyncTaskMethodBuilder.Create(),
			_state = -1
		};
		stateMachine._builder.Start(ref stateMachine);
		return stateMachine._builder.Task;
	}
}

struct MainAsyncStateMachine : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	public TaskAwaiter _waiter;


	public void MoveNext()
	{
		int num1 = this._state;
		try
		{
			TaskAwaiter awaiter;
			int num2;
			if (num1 != 0)
			{
				awaiter = StateMachineFactory.CreateTest1AsyncStateMachine().GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					Console.WriteLine("MainAsyncStateMachine###########Test1AsyncStateMachine IsCompleted:false， 注册自己到Test1Async运行结束时运行");
					this._state = num2 = 0;
					this._waiter = awaiter;
					this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				Console.WriteLine("MainAsyncStateMachine###########Test1AsyncStateMachine IsCompleted:true");
				awaiter = this._waiter;
				this._waiter = new TaskAwaiter();
				this._state = num2 = -1;
			}
			awaiter.GetResult();
			Console.WriteLine("MainAsyncStateMachine###########Let's Go!");
		}
		catch (Exception e)
		{
			this._state = -2;
			this._builder.SetException(e);
			return;
		}

		this._state = -2;
		this._builder.SetResult();
	}

	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this._builder.SetStateMachine(stateMachine);
	}
}

struct Test1AsyncStateMachine : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	public TaskAwaiter _waiter;


	public void MoveNext()
	{
		int num1 = this._state;
		try
		{
			TaskAwaiter awaiter;
			int num2;
			if (num1 != 0)
			{
				Console.WriteLine("Test1AsyncStateMachine###########test1-start");
				awaiter = StateMachineFactory.CreateTest2AsyncStateMachine().GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					Console.WriteLine("Test1AsyncStateMachine###########Test2AsyncStateMachine IsCompleted:false， 注册自己到Test2Async运行结束时运行");
					this._state = num2 = 0;
					this._waiter = awaiter;
					this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				Console.WriteLine("Test1AsyncStateMachine###########Test2AsyncStateMachine IsCompleted:true");
				awaiter = this._waiter;
				this._waiter = new TaskAwaiter();
				this._state = num2 = -1;
			}
			awaiter.GetResult();
			Console.WriteLine("Test1AsyncStateMachine###########test1-end");
		}
		catch (Exception e)
		{
			this._state = -2;
			this._builder.SetException(e);
			return;
		}

		this._state = -2;
		this._builder.SetResult();
	}

	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this._builder.SetStateMachine(stateMachine);
	}
}

struct Test2AsyncStateMachine : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	public TaskAwaiter _waiter;


	public void MoveNext()
	{
		int num1 = this._state;
		try
		{
			TaskAwaiter awaiter;
			int num2;
			if (num1 != 0)
			{
				Console.WriteLine("Test2AsyncStateMachine###########test2-start");
				awaiter = Task.Delay(1000).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					Console.WriteLine("Test2AsyncStateMachine###########Task.Delay(1000) IsCompleted:false , 注册自己到Task.Delay(1000)运行结束时运行");
					this._state = num2 = 0;
					this._waiter = awaiter;
					this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				Console.WriteLine("Test2AsyncStateMachine###########Task.Delay(1000) IsCompleted:true");
				awaiter = this._waiter;
				this._waiter = new TaskAwaiter();
				this._state = num2 = -1;
			}

			awaiter.GetResult();
			Console.WriteLine("Test2AsyncStateMachine###########test2-end");
		}
		catch (Exception e)
		{
			this._state = -2;
			this._builder.SetException(e);
			return;
		}

		this._state = -2;
		this._builder.SetResult();
	}

	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this._builder.SetStateMachine(stateMachine);
	}
}

// static async Task test2()
// {
// 	Console.WriteLine("test2-start");
// 	await Task.Delay(1000);
// 	await Task.Delay(2000);
// 	await Task.Delay(3000);
// 	Console.WriteLine("test2-end");
// }
struct Test3AsyncStateMachine : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	public TaskAwaiter _waiter;
	public void MoveNext()
	{
		int num1 = this._state;
		try
		{
			TaskAwaiter awaiter;
			switch (num1)
			{
				case 0:
					awaiter = this._waiter;
					this._waiter = new TaskAwaiter();
					this._state = -1;
					break;
				case 1:
					awaiter = this._waiter;
					this._waiter = new TaskAwaiter();
					this._state = -1;
					goto label1;
				case 2:
					awaiter = this._waiter;
					this._waiter = new TaskAwaiter();
					this._state = -1;
					goto label2;
				default:
					Console.WriteLine("test2-start");
					awaiter = Task.Delay(1000).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this._state  = 0;
						this._waiter = awaiter;
						this._builder.AwaitUnsafeOnCompleted(ref awaiter,ref this );
						return;
					}
					break;
			}
			
			awaiter.GetResult();
			awaiter = Task.Delay(2000).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				this._state  = 1;
				this._waiter = awaiter;
				this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			label1:
			awaiter.GetResult();
			awaiter = Task.Delay(3000).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				this._state  = 2;
				this._waiter = awaiter;
				this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			label2:
			awaiter.GetResult();
			Console.WriteLine("Test2AsyncStateMachine######test2-end");
		}
		catch (Exception e)
		{
			this._state = -2;
			this._builder.SetException(e);
			return;
		}
		this._state = -2;
		this._builder.SetResult();
	}
	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this._builder.SetStateMachine(stateMachine);
	}
}