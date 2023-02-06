
#### csharp的async&await
demo

```csharp
static async Task Main(string[] args)
{
   await test1();      
   Console.WriteLine("Let's Go!");
}

async Task test1(){
 	Console.WriteLine("test1-start");
 	await test2();
 	Console.WriteLine("test1-end");

 }

async Task test2()
{
 	Console.WriteLine("test2-start");
 	await Task.Delay(1000);
 	Console.WriteLine("test2-end");
 }
```

我们反编译查看下编译器生成了怎样的状态机

![image](https://dimg04.c-ctrip.com/images/0v50w12000a6kyj55EAFD.png)


看反编译的代码比较吃力，我还原成了正常代码，
```csharp
static Task CreateMainAsyncStateMachine()
{
	MainAsyncStateMachine stateMachine = new MainAsyncStateMachine
	{
		_builder = AsyncTaskMethodBuilder.Create(),
		_state = -1
	};
	stateMachine._builder.Start(ref stateMachine);
	return stateMachine._builder.Task;
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
				awaiter = UserQuery.CreateTest1AsyncStateMachine().GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					Console.WriteLine("MainAsyncStateMachine######Test1AsyncStateMachine IsCompleted:false， 注册自己到Test1Async运行结束时运行");
					this._state = num2 = 0;
					this._waiter = awaiter;
					this._builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				Console.WriteLine("MainAsyncStateMachine######Test1AsyncStateMachine IsCompleted:true");
				awaiter = this._waiter;
				this._waiter = new TaskAwaiter();
				this._state = num2 = -1;
			}
			awaiter.GetResult();
			Console.WriteLine("MainAsyncStateMachine######Let's Go!");
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
```
完整代码请查看
[https://github.com/yuzd/asyncawait_study](https://github.com/yuzd/asyncawait_study)

可以看出来，和kotlin其实原理差不多，都是生成一个函数加一个状态机

区别是csharp的函数就是创建一个状态机且启动它


```csharp
// 当状态机启动时会触发 状态机的MoveNext方法的调用
stateMachine._builder.Start(ref stateMachine);
```

![image](https://dimg04.c-ctrip.com/images/0v55y12000a6aawuz9460.png)

整体的执行流程如下

![image](https://dimg04.c-ctrip.com/images/0v56o12000a6kzh6oE94F.png)

ps:最右边的是展示如果有多个await 那么就会对应这个状态机的多个状态
