const Cities = document.querySelector('#All-city');

showcities()
function showcities() {
    axios({
        method: 'get',
        url: `http://127.0.0.1:7197/Weather/GetAllCities`,
    })
        .then(res => showitem(res))
        .catch(err => console.log(err));


    function showitem(res) {
        
        let cities = res.data

        for (i = 0; i < cities.length; i++) {
            axios({
                method: 'get',
                url: `https://api.openweathermap.org/data/2.5/weather?q=${cities[i].city}&units=metric&appid=6ef7648b487b22a1a83952b58d64fdd1`
            }).then(response => displayitem(response))


            function displayitem(response) {
                let WeatherData = response.data
                output = ""
                output += ` < div class="card col-lg-2 portfolio-item filter-app m-2" >
                                <img src="http://openweathermap.org/img/w/${user[" weather"][0]["icon"]}.png " alt = "" >
                                    <div class="portfolio-info">
                                        <h4>${WeatherData.name}</h4>
                                        <p>${WeatherData["main"]["temp"]} °F </p>
                                        <p>${WeatherData["main"]["pressure"]} mmHg</p>
                                        <p>${WeatherData["main"]["humidity"]} %</p>
                                        <p>${WeatherData["weather"][0]["description"]} sky</p>
                                    </div>
                            </div>`

                  

                const data = document.createElement('div')
                data.innerHTML = output
                Cities.appendChild(data)
            }
        }
    }
}