class ConfirmInviteContributorDelete {
  public confirmRemoveContributor(): this {
    cy.get('.govuk-button').eq(0).click()

    return this
  }
}

const confirmInviteContributorDelete = new ConfirmInviteContributorDelete()

export default confirmInviteContributorDelete
