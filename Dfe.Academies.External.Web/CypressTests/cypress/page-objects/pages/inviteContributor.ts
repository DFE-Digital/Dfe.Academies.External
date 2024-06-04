class InviteContributor {
  public fillDetailsAndSubmit(): this {
    cy.fillDetailsAndSubmit()

    return this
  }

  public verifySuccessBannerAndContributorList(): this {
    cy.verifySuccessBannerAndContributorList()

    return this
  }

  public selectRemoveContributorLink(): this {
    cy.contains('Remove contributor').click()

    return this
  }

  public verifyContributorRemovedAndSuccessRemoved(): this {
    cy.verifyContributorRemovedAndSuccessRemoved()

    return this
  }
}

const inviteContributor = new InviteContributor()

export default inviteContributor
