class TrustDetailsSummary {
  // TODO get better selector for element
  public startTrustDetails(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public saveAndReturnToApp(): this {
    cy.url().then((url) => {
      const applicationId = url.substring(
        url.indexOf('=') + 1,
        url.lastIndexOf('&'),
      )
      cy.get(`a[href="/application-overview?appId=${applicationId}"]`).eq(1).click()
    })

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
