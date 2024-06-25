class YourApplications {
  public startNewApplication(): this {
    // TODO get better selector for this
    cy.get('a[href="/what-are-you-applying-to-do"]').click()

    return this
  }

  // TODO fix commented lines
  // TODO fix selectors in this method
  public verifyApplicationDeleted(): this {
    cy.url().then((url) => {
      const applicationId = url.substring(url.indexOf('=') + 1, url.lastIndexOf('&'))

      cy.get('.govuk-body').eq(0).contains(`${applicationId} has been successfully removed.`)
      cy.get('.govuk-table').contains(`${applicationId}`).should('not.exist')
    })

    return this
  }

  public selectApplication(applicationId: string): this {
    cy.contains(applicationId).click()

    return this
  }
}

const yourApplications = new YourApplications()

export default yourApplications
