class InviteContributor {
  public fillDetailsAndSubmit(contributorFirstName: string, contributorEmail: string): this {
    cy.get('#EmailAddress').click()
    cy.get('#EmailAddress').type(contributorEmail)

    cy.get('#Name').click()
    cy.get('#Name').type(contributorFirstName)

    cy.get('#role-2').click()

    cy.get('#role-description').click()
    cy.get('#role-description').type('Headmaster')

    cy.get('input[type=submit]').click()

    return this
  }

  public verifySuccessBannerAndContributorList(contributorFirstName: string): this {
    cy.get('div[role=alert]').contains('Success')
    cy.get('div[role=alert]').contains('Contributor added')
    cy.get('div[role=alert]').contains(`${contributorFirstName} has been sent an invitation to help with this application.`)

    cy.get('.govuk-form-group').contains(`${contributorFirstName}`)
    cy.get('.govuk-form-group').contains('Headmaster')
    cy.get('.govuk-form-group').contains('Remove contributor')

    return this
  }

  public selectRemoveContributorLink(): this {
    cy.contains('Remove contributor').click()

    return this
  }

  public verifyContributorRemovedAndSuccessRemoved(contributorFirstName: string): this {
    cy.get('div[role=alert]').contains('Success')
    cy.get('div[role=alert]').contains('Contributor removed')
    cy.get('div[role=alert]').contains(`${contributorFirstName} can no longer contribute to this application.`)

    cy.get('.govuk-form-group').contains(`${contributorFirstName}`).should('not.exist')

    return this
  }
}

const inviteContributor = new InviteContributor()

export default inviteContributor
