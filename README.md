azure-cloud-service-ga
======================

The code behind <a href="http://sirarscloudservice.cloudapp.net/" target="_blank">http://sirarscloudservice.cloudapp.net/</a>. An Azure cloud service that hosts a genetic algorithm, solving the Traveling Salesman Problem.

In addition to the web interface, it is possible to use the service directly by doing GET requests with <code>[x,y]</code> city coordinates. Here is a GET request example of a TSP with 4 cities:

<code>http://sirarscloudservice.cloudapp.net/Home/Solve?c=200,300&c=400,300&c=400,400&c=400,200</code>

This returns the following JSON with the answer:

<code>
{
  "results":"Solution: 647.214 Path: (1,3,0,2,)"
}
</code>

Solution is the shortest path provided in pixels, and path is the shortest city route (1 = [400,300], 3 = [400,200], etc.).
