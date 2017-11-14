# Quartz.NET

## Overview of using Quartz.NET for scheduling
### How to use Quartz for scheduling tasks (jobs)
<ol>
	<li> Three main componenets for using Quartz.NET scheduling
		<ul>
			<li>The first component is the <b>scheduler</b> itself which is a type implementing <code>IScheduler</code>
				<ul>
					<li>This is a singleton acquired from the factory class <code>StcSchedulerFactory.GetDefaultScheduler()</code></li>
				</ul>
			</li>
      <li>The second compenent is the <b>job</b> which is a type implementing the <code>IJobDetail</code> interface
				<ul>
					<li>This class represents the unit of work we are wanting to use in conjunction with the scheduler so that the unit of work represented by the class is conducted at a specific time</li>
					<li>We specify the unit of work by providing the class we created to encapsulate the task we want scheduled.
						<ul>
							<li>Our "unit of work" class need only implement the <code>IJob</code> interface and provide the unit of work code within an <code>Execute</code> method</li>
						</ul>
					</li>
				</ul>
			</li>
			<li>The Third Component is the <b>trigger</b> that specifies the time at which we wish to peform a <b>job</b>
				<ul>
          <li>This component implements the <code>ITrigger</code> interface and can have time specified at which the associated job will run via the scheduler</li>
				</ul>
			</li>
		</ul>
	</li>
	<li>Once you have all of your components defined you simply set the scheudule up by calling <code>scheduler.ScheduleJob(job,trigger)</code>.	
	</li>
</ol>

### Notes about using Quartz
<ol>
	<li>Quartz.NET is a port of a scheduling library built in java
		<ul>
			<li>Would be hesistant to use this option since the majority of support/documentation is going to be based around frameworks used by Java.</li>
		</ul>
	</li>
	<li>There does not seem to be any hooks to the scheduler that would allow for us to setup monitoring/alarming for the scheduled task execution.
		<ul>
			<li>Found this link https://dzone.com/articles/why-you-shouldnt-use-quartz that brings up some of Quartz's deficiencies and recommends <b>Obsidian Scheduler</b> for Java apps.</li>
		</ul>		
	</li>
</ol>

