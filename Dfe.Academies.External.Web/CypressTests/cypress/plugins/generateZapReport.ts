import zaproxy from 'zaproxy'

export async function generateZapReport() {
  console.log(`Generating ZAP report`)

  const zapOptions = {
    apiKey: process.env.ZAP_API_KEY,
    proxy: {
      host: process.env.ZAP_ADDRESS,
      port: process.env.ZAP_PORT,
    },
  }
  const zap = new zaproxy(zapOptions)
  // Wait for passive scanner to finish scanning before generating report
  let recordsRemaining = 100
  while (recordsRemaining !== 0) {
    await zap.pscan.recordsToScan()
      .then((resp) => {
        try {
          recordsRemaining = parseInt(resp.recordsToScan, 10)
        }
        catch (err) {
          if (err instanceof Error) {
            console.log(`Error converting result: ${err.message}`)
          }
          else {
            console.log('Unknown error during results conversion')
          }
          recordsRemaining = 0
        }
      })
      .catch((err) => {
        console.log(`Error from the ZAP Passive Scan API: ${err}`)
        recordsRemaining = 0
      })
  }

  await zap.reports.generate({
    title: 'Report',
    template: 'traditional-html',
    reportfilename: 'ZAP-Report.html',
    reportdir: '/zap/wrk',
  })
    .then((resp) => {
      console.log(`${JSON.stringify(resp)}`)
    })
    .catch((err) => {
      console.log(`Error from ZAP Report API: ${err}`)
    })
}
