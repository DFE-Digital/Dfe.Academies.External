class YourApplications {
  public selectStartANewApplication(): this {
    cy.get('a[href="/what-are-you-applying-to-do"]').click()

    return this
  }

  public verifyApplicationDeleted(applicationId: string): this {
    cy.log(`Global Application ID = ${applicationId}`)

    // cy.get('.govuk-body').eq(0).contains(`trust A2B_${applicationId} has been successfully removed.`)
    cy.get('.govuk-body').eq(0).contains(`has been successfully removed.`)

    // cy.get('.govuk-table').contains(`${applicationId}`).should('not.exist')

    return this
  }

  public selectApplicationForInviteContributor(): this {
    cy.contains('10280').click()

    return this
  }
}

const yourApplications = new YourApplications()

export default yourApplications
