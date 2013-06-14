--ワイズ・コア
function c100000055.initial_effect(c)
	--special summon
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_DESTROY+CATEGORY_SPECIAL_SUMMON)
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_F)
	e1:SetCode(EVENT_DESTROY)
	e1:SetCondition(c100000055.condition)
	e1:SetOperation(c100000055.operation)
	c:RegisterEffect(e1)
	local e2=e1:Clone()
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c100000055.condition2)
	c:RegisterEffect(e2)
	--battle indes
	local e3=Effect.CreateEffect(c)
	e3:SetType(EFFECT_TYPE_SINGLE)
	e3:SetProperty(EFFECT_FLAG_SINGLE_RANGE)
	e3:SetRange(LOCATION_MZONE)
	e3:SetCode(EFFECT_INDESTRUCTABLE_COUNT)
	e3:SetCountLimit(1)
	e3:SetValue(c100000055.valcon)
	c:RegisterEffect(e3)
end
function c100000055.valcon(e,re,r,rp)
	return bit.band(r,REASON_BATTLE)~=0
end
function c100000055.condition(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return bit.band(c:GetReason(),0x41)==0x41
end
function c100000055.condition2(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return bit.band(c:GetReason(),0x41)==0x41 and c:IsPreviousLocation(LOCATION_MZONE) and not c:IsPreviousPosition(POS_FACEUP)
end
function c100000055.tf1(c,e,tp)
	return c:IsCanBeSpecialSummoned(e,0,c:GetControler(),false,false) and (not c:IsLocation(LOCATION_GRAVE) or not c:IsHasEffect(EFFECT_NECRO_VALLEY))
	and c:IsCode(100000050)
end
function c100000055.tf2(c,e,tp)
	return c:IsCode(100000051) and c:IsCanBeSpecialSummoned(e,0,c:GetControler(),false,false)
		and (not c:IsLocation(LOCATION_GRAVE) or not c:IsHasEffect(EFFECT_NECRO_VALLEY))
end
function c100000055.tf3(c,e,tp)
	return c:IsCode(100000052) and c:IsCanBeSpecialSummoned(e,0,c:GetControler(),false,false)
		and (not c:IsLocation(LOCATION_GRAVE) or not c:IsHasEffect(EFFECT_NECRO_VALLEY))
end
function c100000055.tf4(c,e,tp)
	return c:IsCode(100000053) and c:IsCanBeSpecialSummoned(e,0,c:GetControler(),false,false)
		and (not c:IsLocation(LOCATION_GRAVE) or not c:IsHasEffect(EFFECT_NECRO_VALLEY))
end
function c100000055.tf5(c,e,tp)
	return c:IsCode(100000054) and c:IsCanBeSpecialSummoned(e,0,c:GetControler(),false,false)
		and (not c:IsLocation(LOCATION_GRAVE) or not c:IsHasEffect(EFFECT_NECRO_VALLEY))
end
function c100000055.operation(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local sg=Duel.GetMatchingGroup(Card.IsDestructable,tp,LOCATION_MZONE,0,nil)
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,sg,sg:GetCount(),0,0)
	Duel.Destroy(sg,REASON_EFFECT)
	Duel.BreakEffect()
	if chk==0 then return Duel.GetLocationCount(c:GetControler(),LOCATION_MZONE)>0
		and	Duel.IsExistingMatchingCard(c100000055.tf1,c:GetControler(),0x13,0,1,nil,e,c:GetControler())
		and	Duel.IsExistingMatchingCard(c100000055.tf2,c:GetControler(),0x13,0,1,nil,e,c:GetControler())
		and	Duel.IsExistingMatchingCard(c100000055.tf3,c:GetControler(),0x13,0,1,nil,e,c:GetControler())
		and	Duel.IsExistingMatchingCard(c100000055.tf4,c:GetControler(),0x13,0,1,nil,e,c:GetControler())
		and	Duel.IsExistingMatchingCard(c100000055.tf5,c:GetControler(),0x13,0,1,nil,e,c:GetControler()) end		
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,nil,1,c:GetControler(),0x13)
	Duel.Hint(HINT_SELECTMSG,c:GetControler(),HINTMSG_SPSUMMON)
	local ft=Duel.GetLocationCount(c:GetControler(),LOCATION_MZONE)
		if ft<=0 then return end
	local tc1=Duel.SelectMatchingCard(c:GetControler(),c100000055.tf1,c:GetControler(),0x13,0,1,1,nil,e,c:GetControler())
	if tc1 then
		Duel.SpecialSummon(tc1,0,c:GetControler(),c:GetControler(),false,false,POS_FACEUP)
	end
	if ft<=0 then return end
	local tc2=Duel.SelectMatchingCard(c:GetControler(),c100000055.tf2,c:GetControler(),0x13,0,1,1,nil,e,c:GetControler())
	if tc2 then
		Duel.SpecialSummon(tc2,0,c:GetControler(),c:GetControler(),false,false,POS_FACEUP)
	end
	if ft<=0 then return end
	local tc3=Duel.SelectMatchingCard(c:GetControler(),c100000055.tf3,c:GetControler(),0x13,0,1,1,nil,e,c:GetControler())
	if tc3 then
		Duel.SpecialSummon(tc3,0,c:GetControler(),c:GetControler(),false,false,POS_FACEUP)
	end
	if ft<=0 then return end
	local tc4=Duel.SelectMatchingCard(c:GetControler(),c100000055.tf4,c:GetControler(),0x13,0,1,1,nil,e,c:GetControler())
	if tc4 then
		Duel.SpecialSummon(tc4,0,c:GetControler(),c:GetControler(),false,false,POS_FACEUP)
	end
	if ft<=0 then return end
	local tc5=Duel.SelectMatchingCard(c:GetControler(),c100000055.tf5,c:GetControler(),0x13,0,1,1,nil,e,c:GetControler())
	if tc5 then
		Duel.SpecialSummon(tc5,0,c:GetControler(),c:GetControler(),false,false,POS_FACEUP)
	end
	Duel.ShuffleDeck(c:GetControler())
end
