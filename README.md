<h1>azure-cloud-service-ga</h1>

The code behind <a href="http://sirarscloudservice.cloudapp.net/" target="_blank">http://sirarscloudservice.cloudapp.net/</a>. An Azure cloud service that hosts a genetic algorithm, solving the Traveling Salesman Problem.

In addition to the web UI, it is possible to use the service directly by doing GET requests with <code>[x,y]</code> city coordinates. Here is a GET request example of a TSP with 4 cities:

<code>http://sirarscloudservice.cloudapp.net/Home/Solve?c=200,300&c=400,300&c=400,400&c=400,200</code>

This returns the following JSON:

<code>
{
  "results":"Solution: 647.214 Path: (1,3,0,2,)"
}
</code>

Where solution is the shortest path provided in pixels, and path is the shortest city route (1 is [400,300], 3 is [400,200] and so on).

<h2>Easter Egg: Arma 3 Coordinate Conversion</h2>

The service was later extended with the ability to convert <code>[latitude,longitude]</code> coordinates to <code>[x,y]</code> <a href="http://arma3.com/" target="_blank">Arma 3</a> coordinates for the Altis map. <strong>It must be noted though that this conversion is imprecise</strong> and uses a fixed center location as reference on the Altis map. Here is a GET request example with <code>[latitude,longitude]</code>:

<code>http://sirarscloudservice.cloudapp.net/Home/ConvertToArmaCoordinates?c=39.878880,25.231705</code>

This returns the following JSON:

<code>
{
  "armaCoordinates":[{"X":14306,"Y":13048}]
}
</code>
