const express = require('express')
var bodyParser = require('body-parser')

const app = express()

app.use(bodyParser.json())

app.get('/', (_, res) => {
    res.setHeader('Content-Type', 'text/html')
    res.status(200).send('<h1>The test server is up and running</h1>')
})

app.get('/Ping', (_, res) => {
    const rnd = Math.random()
    let status = 200
    if (rnd > 0.5) status = 500
    console.log({ 'Status: ': status })
    res.status(status).send()
})

app.get('/GetInputData', (_, res) => {
    const body = {'K':10,'Sums':[1.01,2.02],'Muls':[1,4]}
    res.json(body)
})

app.post('/WriteAnswer', (req, res) => {
    var body = JSON.stringify(req.body)
    var expected = JSON.stringify({'SumResult':30.30,'MulResult':4,'SortedInputs':[1.0,1.01,2.02,4.0]})

    if (body === expected) {
        res.status(200).send()
    } else {
        res.status(400).send()
    }
})

app.listen(8080, () => console.log('app is running'))