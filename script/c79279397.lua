--D�ܩ`����
function c79279397.initial_effect(c)
	--spsummon
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(79279397,0))
	e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e1:SetType(EFFECT_TYPE_FLIP+EFFECT_TYPE_SINGLE)
	e1:SetOperation(c79279397.spop)
	c:RegisterEffect(e1)
end
function c79279397.filter(c,e,tp)
	return c:IsCode(79279397) and c:IsCanBeSpecialSummoned(e,0,tp,false,false)
end
function c79279397.spop(e,tp,eg,ep,ev,re,r,rp)
	local ft=Duel.GetLocationCount(tp,LOCATION_MZONE)
	local g=Duel.GetMatchingGroup(c79279397.filter,tp,LOCATION_DECK,0,nil,e,tp)
	if g:GetCount()>0 and Duel.SelectYesNo(tp,aux.Stringid(79279397,1)) then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SPSUMMON)
		local sg=g:Select(tp,1,ft,nil)
		Duel.SpecialSummon(sg,0,tp,tp,false,false,POS_FACEUP_ATTACK)
		Duel.Damage(tp,sg:GetCount()*1000,REASON_EFFECT)
	end
end
