class YourApplications {
  public startNewApplication(): this {
    cy.get('[data-cy="startNewApplicationButton"]').click()

    return this
  }

  public verifyApplicationDeleted(): this {
    cy.url().then((url) => {
      const applicationId = url.substring(url.indexOf('=') + 1, url.lastIndexOf('&'))

      cy.get('[data-cy="successBanner"]').contains(`${applicationId} has been successfully removed.`)
      cy.get('[data-cy="yourApplications"]').contains(`${applicationId}`).should('not.exist')
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
